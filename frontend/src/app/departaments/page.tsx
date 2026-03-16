"use client"

import { useState } from "react"
import { useQuery } from "@tanstack/react-query"
import { departamentsApi, Envelope } from "@/entities/departaments/api"
import { DepartamentChild, DepartamenthAndChildDto } from "@/entities/departaments/types"
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/shared/components/ui/pagination"

const PAGE_SIZE = 5

export default function DepartamentsPage() {
  const [page, setPage] = useState(1)
  const { data, isLoading, error } = useQuery<Envelope<DepartamenthAndChildDto>, Error>({
    queryKey: ["departaments", page],
    queryFn: () => departamentsApi.getDepartaments({ page, pageSize: PAGE_SIZE, prefetch: 3 }),
    staleTime: 1000 * 60,
  })

  const allModels: DepartamentChild[] = data?.result?.departamenthAndChildModels ?? []
  const total = allModels.length
  const lastPage = Math.max(1, Math.ceil(total / PAGE_SIZE))
  const pages = Array.from({ length: lastPage }, (_v, i) => i + 1)
  const models = allModels.slice((page - 1) * PAGE_SIZE, page * PAGE_SIZE)

  return (
    <div className="bg-background text-foreground min-h-screen p-3 sm:p-5 md:p-7 lg:p-8">
      <div className="mx-auto max-w-6xl space-y-4">
        <div className="rounded-2xl border border-border bg-card p-4 sm:p-5">
          <div className="flex flex-wrap items-start justify-between gap-3">
            <div>
              <p className="text-xs uppercase tracking-[0.25em] text-muted-foreground">
                Локации / Департаменты
              </p>
              <h1 className="mt-1 text-2xl font-bold sm:text-3xl">Департаменты</h1>
              <p className="mt-1 max-w-3xl text-sm text-muted-foreground sm:text-base">
                Список корневых департаментов. Карточки адаптируются под экран и длинный текст.
              </p>
            </div>
            <div className="text-xs sm:text-sm">
              Всего департаментов: {total}
            </div>
          </div>
        </div>

        {isLoading ? (
          <div className="rounded-2xl border border-border bg-card p-4 text-center text-muted-foreground">
            Загрузка департаментов...
          </div>
        ) : error ? (
          <div className="rounded-2xl border border-destructive/40 bg-destructive/10 p-4 text-center text-destructive">
            Ошибка загрузки. Обновите страницу.
          </div>
        ) : models.length === 0 ? (
          <div className="rounded-2xl border border-border bg-card p-4 text-center text-muted-foreground">
            Департаменты не найдены.
          </div>
        ) : (
          <>
            <div className="grid gap-3 sm:grid-cols-2 xl:grid-cols-3">
              {models.map((item) => (
                <article
                  key={item.id}
                  className="flex min-h-45 flex-col justify-between rounded-2xl border border-border bg-card p-3 sm:p-4 transition hover:-translate-y-0.5 hover:shadow-md"
                >
                  <div className="space-y-2">
                    <div className="flex flex-wrap items-start justify-between gap-2">
                      <div className="min-w-0">
                        <h2 className="text-base font-semibold leading-snug sm:text-lg truncate">{item.name}</h2>
                        <p className="mt-0.5 text-xs text-muted-foreground sm:text-sm truncate">
                          {item.identifier} · {item.path}
                        </p>
                      </div>
                      <span
                        className={`rounded-full px-2 py-0.5 text-[10px] font-semibold sm:text-xs ${
                          item.isActive ? "bg-emerald-100 text-emerald-700" : "bg-slate-200 text-slate-600"
                        }`}
                      >
                        {item.isActive ? "Активен" : "Неактивен"}
                      </span>
                    </div>
                    <div className="grid gap-2 text-xs text-muted-foreground sm:grid-cols-2 sm:text-sm">
                      <div className="rounded-md bg-muted/60 p-2"><span className="font-medium text-foreground">Глубина:</span> {item.depth}</div>
                      <div className="rounded-md bg-muted/60 p-2"><span className="font-medium text-foreground">Родитель:</span> {item.parentId ?? "—"}</div>
                      <div className="rounded-md bg-muted/60 p-2"><span className="font-medium text-foreground">Детей:</span> {item.childs?.length ?? 0}</div>
                      <div className="rounded-md bg-muted/60 p-2"><span className="font-medium text-foreground">hasMore:</span> {item.hasMoreChildren ? "Да" : "Нет"}</div>
                    </div>
                  </div>
                  <div className="mt-2 rounded-xl border border-border p-2 text-xs text-muted-foreground sm:text-sm">
                    {item.childs?.length ? (
                      <>
                        <div className="font-medium text-foreground">Дочерние департаменты:</div>
                        <ul className="ml-3 mt-1 list-disc space-y-1">
                          {item.childs.slice(0, 3).map((child) => (
                            <li key={child.id} className="truncate">{child.name}</li>
                          ))}
                          {item.childs.length > 3 && <li>+{item.childs.length - 3} ещё...</li>}
                        </ul>
                      </>
                    ) : (
                      <div>Нет дочерних</div>
                    )}
                  </div>
                </article>
              ))}
            </div>

            <Pagination className="mt-4">
              <PaginationPrevious
                onClick={() => setPage((prev) => Math.max(1, prev - 1))}
                text="Назад"
              />
              <PaginationContent>
                {pages.map((p) => (
                  <PaginationItem key={p}>
                    <PaginationLink
                      href="#"
                      isActive={p === page}
                      onClick={(event) => {
                        event.preventDefault()
                        setPage(p)
                      }}
                    >
                      {p}
                    </PaginationLink>
                  </PaginationItem>
                ))}
              </PaginationContent>
              <PaginationNext
                onClick={() => setPage((prev) => Math.min(lastPage, prev + 1))}
                text="Далее"
              />
            </Pagination>
          </>
        )}
      </div>
    </div>
  )
}
