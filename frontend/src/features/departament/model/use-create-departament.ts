import { departamentsApi, Envelope } from "@/entities/departaments/api"
import { DepartamenthAndChildDto } from "@/entities/departaments/types"
import { useQuery } from "@tanstack/react-query"

export default function DepartamentRootList({page, pageSize}: {page: number, pageSize: number}) {

    const { data, isLoading, error } = useQuery<Envelope<DepartamenthAndChildDto>, Error>({
    queryKey: ["departaments", page],
    queryFn: () => departamentsApi.getDepartamentsRoot({ page, pageSize: pageSize, prefetch: 3 }),
    staleTime: 1000 * 60,

    
  })

  return{
    data,
    isLoading,
    error
  }
}