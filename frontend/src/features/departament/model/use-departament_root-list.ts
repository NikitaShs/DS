import { departamentsApi, Envelope } from "@/entities/departaments/api"
import { queryClient } from "@/shared/api/query-client"
import { useMutation } from "@tanstack/react-query"
import { toast } from "sonner"

export function UseCreateDepartament() {
    const mutation = useMutation({
        mutationFn: departamentsApi.postDepartament,
    onSettled: () => queryClient.invalidateQueries({ queryKey: ["departaments"] }),
    onSuccess: () => {
        toast.success("Департамент успешно создан")
    },
    onError: () => {
        toast.error("Ошибка при создании департамента")
    }
        })

  return{
    createDepartament: mutation.mutateAsync,
    isPending: mutation.isPending,
    error: mutation.error
  }
}