import { apiClient } from "@/shared/api/axion-instance";
import axios from "axios";
import { DepartamenthAndChildDto } from "./types";


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

export type CreateDepartament = {
    name: string;
    identifier: string;
    locationsId: string[];
    parantId?: string;
}

export const departamentsApi = {
    getDepartamentsRoot: async (request : paginationRootsDepth): Promise<Envelope<DepartamenthAndChildDto>> => {
        const response = await apiClient.get<Envelope<DepartamenthAndChildDto>>("/api/departaments/roots", {
            params: {
                SizePage: request.pageSize,
                Page: request.page,
                prefetch: request.prefetch,
            },
        });

        return response.data;
    },

    postDepartament: async (request : CreateDepartament): Promise<Envelope<string>> => {
        const response = await apiClient.post<Envelope<string>>("/api/departaments", request);

        return response.data;
    }
}

