"use client"

import { Pagination, PaginationContent, PaginationItem, PaginationLink, PaginationNext, PaginationPrevious } from "@/shared/components/ui/pagination";

import { Dispatch, SetStateAction } from "react";

type DepartamentsPaginationProps = {
  page: number;
  setPage: Dispatch<SetStateAction<number>>;
  total: number;
  pageSize: number;
};

export function DepartamentsPagination({ page, setPage, total, pageSize }: DepartamentsPaginationProps) {
  const lastPage = Math.max(1, Math.ceil(total / pageSize));
  const pages = Array.from({ length: lastPage }, (_v, i) => i + 1);

  return (
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
                event.preventDefault();
                setPage(p);
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
  );
}
