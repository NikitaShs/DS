(globalThis.TURBOPACK || (globalThis.TURBOPACK = [])).push([typeof document === "object" ? document.currentScript : undefined,
"[project]/src/shared/api/axion-instance.ts [app-client] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "apiClient",
    ()=>apiClient
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$axios$2f$lib$2f$axios$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/axios/lib/axios.js [app-client] (ecmascript)");
;
const apiClient = __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$axios$2f$lib$2f$axios$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["default"].create({
    baseURL: "http://localhost:5053",
    headers: {
        "Content-Type": "application/json"
    }
});
if (typeof globalThis.$RefreshHelpers$ === 'object' && globalThis.$RefreshHelpers !== null) {
    __turbopack_context__.k.registerExports(__turbopack_context__.m, globalThis.$RefreshHelpers$);
}
}),
"[project]/src/entities/departaments/api.ts [app-client] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "departamentsApi",
    ()=>departamentsApi
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$api$2f$axion$2d$instance$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/src/shared/api/axion-instance.ts [app-client] (ecmascript)");
;
const departamentsApi = {
    getDepartaments: async (request)=>{
        const response = await __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$api$2f$axion$2d$instance$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["apiClient"].get("/api/departaments/roots", {
            params: {
                SizePage: request.pageSize,
                Page: request.page,
                prefetch: request.prefetch
            }
        });
        return response.data;
    }
};
if (typeof globalThis.$RefreshHelpers$ === 'object' && globalThis.$RefreshHelpers !== null) {
    __turbopack_context__.k.registerExports(__turbopack_context__.m, globalThis.$RefreshHelpers$);
}
}),
"[project]/src/app/departaments/page.tsx [app-client] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "default",
    ()=>DepartamentsPage
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/jsx-dev-runtime.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/compiler-runtime.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$index$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/index.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$tanstack$2f$react$2d$query$2f$build$2f$modern$2f$useQuery$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/@tanstack/react-query/build/modern/useQuery.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$entities$2f$departaments$2f$api$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/src/entities/departaments/api.ts [app-client] (ecmascript)");
;
var _s = __turbopack_context__.k.signature();
"use client";
;
;
;
;
const PAGE_SIZE = 5;
function DepartamentsPage() {
    _s();
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(27);
    if ($[0] !== "c3f2475342705764a96ad8ffe0d6f38d0261453b225a83ffa22bf1e4e78057d6") {
        for(let $i = 0; $i < 27; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "c3f2475342705764a96ad8ffe0d6f38d0261453b225a83ffa22bf1e4e78057d6";
    }
    const [page, setPage] = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$index$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useState"])(1);
    let t0;
    if ($[1] !== page) {
        t0 = {
            queryKey: [
                "departaments",
                page
            ],
            queryFn: ()=>__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$entities$2f$departaments$2f$api$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["departamentsApi"].getDepartaments({
                    page,
                    pageSize: PAGE_SIZE,
                    prefetch: 3
                }),
            staleTime: 60000
        };
        $[1] = page;
        $[2] = t0;
    } else {
        t0 = $[2];
    }
    const { data, isLoading, error } = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$tanstack$2f$react$2d$query$2f$build$2f$modern$2f$useQuery$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useQuery"])(t0);
    let t1;
    if ($[3] !== data?.result?.departamenthAndChildModels) {
        t1 = data?.result?.departamenthAndChildModels ?? [];
        $[3] = data?.result?.departamenthAndChildModels;
        $[4] = t1;
    } else {
        t1 = $[4];
    }
    const allModels = t1;
    const total = data?.result?.totalCount ?? allModels.length;
    const lastPage = Math.max(1, Math.ceil(total / PAGE_SIZE));
    let t2;
    if ($[5] !== lastPage) {
        t2 = Array.from({
            length: lastPage
        }, _DepartamentsPageArrayFrom);
        $[5] = lastPage;
        $[6] = t2;
    } else {
        t2 = $[6];
    }
    const pages = t2;
    let t3;
    if ($[7] !== allModels || $[8] !== page) {
        t3 = allModels.length > PAGE_SIZE ? allModels.slice((page - 1) * PAGE_SIZE, page * PAGE_SIZE) : allModels;
        $[7] = allModels;
        $[8] = page;
        $[9] = t3;
    } else {
        t3 = $[9];
    }
    const models = t3;
    let t4;
    let t5;
    if ($[10] !== lastPage || $[11] !== page) {
        t4 = ({
            "DepartamentsPage[useEffect()]": ()=>{
                if (page > lastPage) {
                    setPage(lastPage);
                }
                if (page < 1) {
                    setPage(1);
                }
            }
        })["DepartamentsPage[useEffect()]"];
        t5 = [
            page,
            lastPage
        ];
        $[10] = lastPage;
        $[11] = page;
        $[12] = t4;
        $[13] = t5;
    } else {
        t4 = $[12];
        t5 = $[13];
    }
    (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$index$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useEffect"])(t4, t5);
    let t6;
    if ($[14] === Symbol.for("react.memo_cache_sentinel")) {
        t6 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            children: [
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                    className: "text-xs uppercase tracking-[0.25em] text-muted-foreground",
                    children: "Локации / Департаменты"
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 97,
                    columnNumber: 15
                }, this),
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("h1", {
                    className: "mt-1 text-2xl font-bold sm:text-3xl",
                    children: "Департаменты"
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 97,
                    columnNumber: 114
                }, this),
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                    className: "mt-1 max-w-3xl text-sm text-muted-foreground sm:text-base",
                    children: "Список корневых департаментов. Карточки адаптируются под экран и длинный текст."
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 97,
                    columnNumber: 183
                }, this)
            ]
        }, void 0, true, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 97,
            columnNumber: 10
        }, this);
        $[14] = t6;
    } else {
        t6 = $[14];
    }
    let t7;
    if ($[15] !== total) {
        t7 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-border bg-card p-4 sm:p-5",
            children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "flex flex-wrap items-start justify-between gap-3",
                children: [
                    t6,
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                        className: "text-xs sm:text-sm",
                        children: [
                            "Всего департаментов: ",
                            total
                        ]
                    }, void 0, true, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 104,
                        columnNumber: 149
                    }, this)
                ]
            }, void 0, true, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 104,
                columnNumber: 79
            }, this)
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 104,
            columnNumber: 10
        }, this);
        $[15] = total;
        $[16] = t7;
    } else {
        t7 = $[16];
    }
    let t8;
    if ($[17] !== error || $[18] !== isLoading || $[19] !== lastPage || $[20] !== models || $[21] !== page || $[22] !== pages) {
        t8 = isLoading ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-border bg-card p-4 text-center text-muted-foreground",
            children: "Загрузка департаментов..."
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 112,
            columnNumber: 22
        }, this) : error ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-destructive/40 bg-destructive/10 p-4 text-center text-destructive",
            children: "Ошибка загрузки. Обновите страницу."
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 112,
            columnNumber: 160
        }, this) : models.length === 0 ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-border bg-card p-4 text-center text-muted-foreground",
            children: "Департаменты не найдены."
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 112,
            columnNumber: 335
        }, this) : /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["Fragment"], {
            children: [
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                    className: "grid gap-3 sm:grid-cols-2 xl:grid-cols-3",
                    children: models.map(_DepartamentsPageModelsMap)
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 112,
                    columnNumber: 466
                }, this),
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(Pagination, {
                    className: "mt-4",
                    children: [
                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(PaginationPrevious, {
                            onClick: {
                                "DepartamentsPage[<PaginationPrevious>.onClick]": ()=>setPage(_DepartamentsPagePaginationPreviousOnClickSetPage)
                            }["DepartamentsPage[<PaginationPrevious>.onClick]"],
                            text: "\u041D\u0430\u0437\u0430\u0434"
                        }, void 0, false, {
                            fileName: "[project]/src/app/departaments/page.tsx",
                            lineNumber: 112,
                            columnNumber: 599
                        }, this),
                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(PaginationContent, {
                            children: pages.map({
                                "DepartamentsPage[pages.map()]": (p)=>/*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(PaginationItem, {
                                        children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(PaginationLink, {
                                            href: "#",
                                            isActive: p === page,
                                            onClick: {
                                                "DepartamentsPage[pages.map() > <PaginationLink>.onClick]": (event)=>{
                                                    event.preventDefault();
                                                    setPage(p);
                                                }
                                            }["DepartamentsPage[pages.map() > <PaginationLink>.onClick]"],
                                            children: p
                                        }, void 0, false, {
                                            fileName: "[project]/src/app/departaments/page.tsx",
                                            lineNumber: 115,
                                            columnNumber: 75
                                        }, this)
                                    }, p, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 115,
                                        columnNumber: 51
                                    }, this)
                            }["DepartamentsPage[pages.map()]"])
                        }, void 0, false, {
                            fileName: "[project]/src/app/departaments/page.tsx",
                            lineNumber: 114,
                            columnNumber: 104
                        }, this),
                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(PaginationNext, {
                            onClick: {
                                "DepartamentsPage[<PaginationNext>.onClick]": ()=>setPage({
                                        "DepartamentsPage[<PaginationNext>.onClick > setPage()]": (prev_0)=>Math.min(lastPage, prev_0 + 1)
                                    }["DepartamentsPage[<PaginationNext>.onClick > setPage()]"])
                            }["DepartamentsPage[<PaginationNext>.onClick]"],
                            text: "\u0414\u0430\u043B\u0435\u0435"
                        }, void 0, false, {
                            fileName: "[project]/src/app/departaments/page.tsx",
                            lineNumber: 121,
                            columnNumber: 67
                        }, this)
                    ]
                }, void 0, true, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 112,
                    columnNumber: 570
                }, this)
            ]
        }, void 0, true);
        $[17] = error;
        $[18] = isLoading;
        $[19] = lastPage;
        $[20] = models;
        $[21] = page;
        $[22] = pages;
        $[23] = t8;
    } else {
        t8 = $[23];
    }
    let t9;
    if ($[24] !== t7 || $[25] !== t8) {
        t9 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "bg-background text-foreground min-h-screen p-3 sm:p-5 md:p-7 lg:p-8",
            children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "mx-auto max-w-6xl space-y-4",
                children: [
                    t7,
                    t8
                ]
            }, void 0, true, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 138,
                columnNumber: 95
            }, this)
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 138,
            columnNumber: 10
        }, this);
        $[24] = t7;
        $[25] = t8;
        $[26] = t9;
    } else {
        t9 = $[26];
    }
    return t9;
}
_s(DepartamentsPage, "DgHq2+ts1guXzJHVKhEuvQkzyJQ=", false, function() {
    return [
        __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$tanstack$2f$react$2d$query$2f$build$2f$modern$2f$useQuery$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useQuery"]
    ];
});
_c = DepartamentsPage;
function _DepartamentsPagePaginationPreviousOnClickSetPage(prev) {
    return Math.max(1, prev - 1);
}
function _DepartamentsPageModelsMap(item) {
    return /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("article", {
        className: "flex min-h-45 flex-col justify-between rounded-2xl border border-border bg-card p-3 sm:p-4 transition hover:-translate-y-0.5 hover:shadow-md",
        children: [
            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "space-y-2",
                children: [
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                        className: "flex flex-wrap items-start justify-between gap-2",
                        children: [
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "min-w-0",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("h2", {
                                        className: "text-base font-semibold leading-snug sm:text-lg truncate",
                                        children: item.name
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 151,
                                        columnNumber: 304
                                    }, this),
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                                        className: "mt-0.5 text-xs text-muted-foreground sm:text-sm truncate",
                                        children: [
                                            item.identifier,
                                            " · ",
                                            item.path
                                        ]
                                    }, void 0, true, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 151,
                                        columnNumber: 393
                                    }, this)
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 151,
                                columnNumber: 279
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                className: `rounded-full px-2 py-0.5 text-[10px] font-semibold sm:text-xs ${item.isActive ? "bg-emerald-100 text-emerald-700" : "bg-slate-200 text-slate-600"}`,
                                children: item.isActive ? "\u0410\u043A\u0442\u0438\u0432\u0435\u043D" : "\u041D\u0435\u0430\u043A\u0442\u0438\u0432\u0435\u043D"
                            }, void 0, false, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 151,
                                columnNumber: 506
                            }, this)
                        ]
                    }, void 0, true, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 151,
                        columnNumber: 213
                    }, this),
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                        className: "grid gap-2 text-xs text-muted-foreground sm:grid-cols-2 sm:text-sm",
                        children: [
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "rounded-md bg-muted/60 p-2",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                        className: "font-medium text-foreground",
                                        children: "Глубина:"
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 151,
                                        columnNumber: 935
                                    }, this),
                                    " ",
                                    item.depth
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 151,
                                columnNumber: 891
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "rounded-md bg-muted/60 p-2",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                        className: "font-medium text-foreground",
                                        children: "Родитель:"
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 151,
                                        columnNumber: 1059
                                    }, this),
                                    " ",
                                    item.parentId ?? "\u2014"
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 151,
                                columnNumber: 1015
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "rounded-md bg-muted/60 p-2",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                        className: "font-medium text-foreground",
                                        children: "Детей:"
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 151,
                                        columnNumber: 1199
                                    }, this),
                                    " ",
                                    item.childs?.length ?? 0
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 151,
                                columnNumber: 1155
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "rounded-md bg-muted/60 p-2",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                        className: "font-medium text-foreground",
                                        children: "hasMore:"
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 151,
                                        columnNumber: 1335
                                    }, this),
                                    " ",
                                    item.hasMoreChildren ? "\u0414\u0430" : "\u041D\u0435\u0442"
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 151,
                                columnNumber: 1291
                            }, this)
                        ]
                    }, void 0, true, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 151,
                        columnNumber: 807
                    }, this)
                ]
            }, void 0, true, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 151,
                columnNumber: 186
            }, this),
            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "mt-2 rounded-xl border border-border p-2 text-xs text-muted-foreground sm:text-sm",
                children: item.childs?.length ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["Fragment"], {
                    children: [
                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                            className: "font-medium text-foreground",
                            children: "Дочерние департаменты:"
                        }, void 0, false, {
                            fileName: "[project]/src/app/departaments/page.tsx",
                            lineNumber: 151,
                            columnNumber: 1601
                        }, this),
                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("ul", {
                            className: "ml-3 mt-1 list-disc space-y-1",
                            children: [
                                item.childs.slice(0, 3).map(_DepartamentsPageModelsMapAnonymous),
                                item.childs.length > 3 && /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("li", {
                                    children: [
                                        "+",
                                        item.childs.length - 3,
                                        " ещё..."
                                    ]
                                }, void 0, true, {
                                    fileName: "[project]/src/app/departaments/page.tsx",
                                    lineNumber: 151,
                                    columnNumber: 1813
                                }, this)
                            ]
                        }, void 0, true, {
                            fileName: "[project]/src/app/departaments/page.tsx",
                            lineNumber: 151,
                            columnNumber: 1674
                        }, this)
                    ]
                }, void 0, true) : /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                    children: "Нет дочерних"
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 151,
                    columnNumber: 1866
                }, this)
            }, void 0, false, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 151,
                columnNumber: 1477
            }, this)
        ]
    }, item.id, true, {
        fileName: "[project]/src/app/departaments/page.tsx",
        lineNumber: 151,
        columnNumber: 10
    }, this);
}
function _DepartamentsPageModelsMapAnonymous(child) {
    return /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("li", {
        className: "truncate",
        children: child.name
    }, child.id, false, {
        fileName: "[project]/src/app/departaments/page.tsx",
        lineNumber: 154,
        columnNumber: 10
    }, this);
}
function _DepartamentsPageArrayFrom(_v, i) {
    return i + 1;
}
var _c;
__turbopack_context__.k.register(_c, "DepartamentsPage");
if (typeof globalThis.$RefreshHelpers$ === 'object' && globalThis.$RefreshHelpers !== null) {
    __turbopack_context__.k.registerExports(__turbopack_context__.m, globalThis.$RefreshHelpers$);
}
}),
]);

//# sourceMappingURL=src_bd3f4259._.js.map