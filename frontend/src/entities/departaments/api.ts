import { apiClient } from "@/shared/api/axion-instance";
import axios from "axios";


export type pagination = {
    search?: string,
    page: number
    pageSize: number
}

export type paginationRootsDepth = {
    prefetch?: number,
    page: number
    pageSize: number
}

export type Envelope<T> = {
    result: T;
    errorList: null;
    timeGeneral: string;
}

export const departamentsApi = {
    getDepartaments: async (request : paginationRootsDepth): Promise<Envelope<DepartamenthAndChildDto>> => {
        const response = await apiClient.get<Envelope<DepartamenthAndChildDto>>("/api/departaments/roots",{
            params: request
        });

        return response.data;
    }
}