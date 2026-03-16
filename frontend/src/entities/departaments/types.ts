export type Departament = {
    id: string;
    name: string;
    identifier: string;
    parentId?: string;
    depth: number;
    path : string;
    isActive: boolean;
    hasMoreChildren: boolean;
    //media?: MediaDto[];
}

export type DepartamentChild = {
    id: string;
    name: string;
    identifier: string;
    parentId?: string;
    depth: number;
    path : string;
    isActive: boolean;
    hasMoreChildren: boolean;
    childs?: Departament[];
    //media?: MediaDto[];
}

export type MediaDto = {
    id: string;
    videoUrl: string;
    status: string;
}

export type DepartamenthAndChildDto = {
    departamenthAndChildModels: DepartamentChild[];
    totalCount: number;
}
