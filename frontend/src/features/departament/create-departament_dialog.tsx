"use client"

import { useState } from "react"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import { z } from "zod"
import { Button } from "@/shared/components/ui/button"
import { Input } from "@/shared/components/ui/input"
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/shared/components/ui/dialog"
import { UseCreateDepartament } from "./model/use-departament_root-list"

const createDepartamentSchema = z.object({
  name: z.string().min(1, "Название обязательно"),
  identifier: z.string().min(1, "Идентификатор обязателен"),
  locationIds: z.string().min(1, "Укажите хотя бы один ID локации").refine(
    (val) => val.split(",").map(id => id.trim()).filter(id => id !== "").length > 0,
    { message: "Укажите хотя бы один валидный ID локации" }
  ),
  parentId: z.string().optional(),
})

export type CreateDepartamentFormValues = z.infer<typeof createDepartamentSchema>

type CreateDepartamentDialogProps = {
  onSubmit?: (values: CreateDepartamentFormValues) => void
}

export function CreateDepartamentDialog({ onSubmit }: CreateDepartamentDialogProps) {
  const [open, setOpen] = useState(false)
  const { createDepartament, isPending } = UseCreateDepartament();

  const {
    register,
    handleSubmit: hookFormSubmit,
    formState: { errors },
    reset,
  } = useForm<CreateDepartamentFormValues>({
    resolver: zodResolver(createDepartamentSchema),
    defaultValues: {
      name: "",
      identifier: "",
      locationIds: "",
      parentId: "",
    },
  })

  const onSubmitForm = async (data: CreateDepartamentFormValues) => {
    try {
      await createDepartament({
        name: data.name,
        identifier: data.identifier,
        locationsId: data.locationIds.split(",").map(id => id.trim()).filter(id => id !== ""),
        parantId: data.parentId || undefined,
      })
      reset()
      setOpen(false)
    } catch (e) {
      console.error(e)
    }
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger>
        <Button variant="default">Открыть диалог департамента</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Создать департамент</DialogTitle>
          <DialogDescription>
            Введите данные департамента (сама отправка запроса реализуется в родителе).
          </DialogDescription>
        </DialogHeader>

        <form className="mt-3 space-y-3" onSubmit={hookFormSubmit(onSubmitForm)}>
          <div className="space-y-1">
            <label className="block text-xs font-medium text-muted-foreground">Название</label>
            <Input
              {...register("name")}
              placeholder="Название"
            />
            {errors.name && (
              <p className="text-xs text-red-500">{errors.name.message}</p>
            )}
          </div>

          <div className="space-y-1">
            <label className="block text-xs font-medium text-muted-foreground">Идентификатор</label>
            <Input
              {...register("identifier")}
              placeholder="Идентификатор"
            />
            {errors.identifier && (
              <p className="text-xs text-red-500">{errors.identifier.message}</p>
            )}
          </div>

          <div className="space-y-1">
            <label className="block text-xs font-medium text-muted-foreground">ID локаций (через запятую)</label>
            <Input
              {...register("locationIds")}
              placeholder="id1,id2"
            />
            {errors.locationIds && (
              <p className="text-xs text-red-500">{errors.locationIds.message}</p>
            )}
          </div>

          <div className="space-y-1">
            <label className="block text-xs font-medium text-muted-foreground">ID родительского департамента (опционально)</label>
            <Input
              {...register("parentId")}
              placeholder="parentId"
            />
            {errors.parentId && (
              <p className="text-xs text-red-500">{errors.parentId.message}</p>
            )}
          </div>

          <DialogFooter>
            <Button type="submit" variant="default" disabled={isPending}>
              {isPending ? "Создание..." : "Создать"}
            </Button>
            <DialogClose render={<Button variant="outline">Отмена</Button>} />
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}
