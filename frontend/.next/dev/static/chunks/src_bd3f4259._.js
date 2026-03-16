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
            params: request
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
function getPagerPages(current, last) {
    const pages = [];
    for(let i = 1; i <= last; i += 1){
        if (last <= 5 || i === 1 || i === last || Math.abs(i - current) <= 1) {
            pages.push(i);
        } else if (i === 2 || i === last - 1) {
            pages.push("...");
        }
    }
    return pages.filter((page, index)=>page !== "..." || pages[index - 1] !== "...");
}
function DepartamentsPage() {
    _s();
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(56);
    if ($[0] !== "d55e3516f4cf683a2b3a40464f4436a3617fa80f744738c82dbf26c131acd5e0") {
        for(let $i = 0; $i < 56; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "d55e3516f4cf683a2b3a40464f4436a3617fa80f744738c82dbf26c131acd5e0";
    }
    const [page, setPage] = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$index$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useState"])(1);
    let t0;
    let t1;
    if ($[1] !== page) {
        t0 = [
            "departaments-roots",
            page
        ];
        t1 = ({
            "DepartamentsPage[useQuery()]": ()=>__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$entities$2f$departaments$2f$api$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["departamentsApi"].getDepartaments({
                    page,
                    pageSize: PAGE_SIZE,
                    prefetch: 3
                })
        })["DepartamentsPage[useQuery()]"];
        $[1] = page;
        $[2] = t0;
        $[3] = t1;
    } else {
        t0 = $[2];
        t1 = $[3];
    }
    let t2;
    if ($[4] === Symbol.for("react.memo_cache_sentinel")) {
        t2 = {
            keepPreviousData: true,
            staleTime: 60000
        };
        $[4] = t2;
    } else {
        t2 = $[4];
    }
    const { data, isLoading, error } = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$tanstack$2f$react$2d$query$2f$build$2f$modern$2f$useQuery$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useQuery"])(t0, t1, t2);
    let t3;
    if ($[5] !== data?.result?.departamenthAndChildModels) {
        t3 = data?.result?.departamenthAndChildModels ?? [];
        $[5] = data?.result?.departamenthAndChildModels;
        $[6] = t3;
    } else {
        t3 = $[6];
    }
    const models = t3;
    const total = data?.result?.totalCount ?? 0;
    const lastPage = Math.max(1, Math.ceil(total / PAGE_SIZE));
    let t10;
    let t4;
    let t5;
    let t6;
    let t7;
    let t8;
    let t9;
    if ($[7] !== isLoading || $[8] !== lastPage || $[9] !== page || $[10] !== total) {
        const pagerPages = getPagerPages(page, lastPage);
        t10 = "bg-background text-foreground min-h-screen p-4 sm:p-6 md:p-8";
        t9 = "mx-auto max-w-6xl space-y-4";
        t7 = "rounded-2xl border border-border bg-card p-4 sm:p-5";
        let t11;
        if ($[18] === Symbol.for("react.memo_cache_sentinel")) {
            t11 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                children: [
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                        className: "text-xs uppercase tracking-[0.25em] text-muted-foreground",
                        children: "Locations / Departments"
                    }, void 0, false, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 87,
                        columnNumber: 18
                    }, this),
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("h1", {
                        className: "mt-1 text-2xl font-bold sm:text-3xl",
                        children: "Departments"
                    }, void 0, false, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 87,
                        columnNumber: 118
                    }, this),
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                        className: "mt-1 max-w-3xl text-sm text-muted-foreground sm:text-base",
                        children: "Root departments list. Showing up to 5 items per page."
                    }, void 0, false, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 87,
                        columnNumber: 186
                    }, this)
                ]
            }, void 0, true, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 87,
                columnNumber: 13
            }, this);
            $[18] = t11;
        } else {
            t11 = $[18];
        }
        if ($[19] !== total) {
            t8 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "flex flex-col gap-2 sm:flex-row sm:items-end sm:justify-between",
                children: [
                    t11,
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                        className: "rounded-xl border border-border bg-muted px-3 py-2 text-xs font-medium text-foreground",
                        children: [
                            "Total: ",
                            total
                        ]
                    }, void 0, true, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 93,
                        columnNumber: 98
                    }, this)
                ]
            }, void 0, true, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 93,
                columnNumber: 12
            }, this);
            $[19] = total;
            $[20] = t8;
        } else {
            t8 = $[20];
        }
        t4 = "mt-3 flex flex-wrap items-center gap-2";
        let t12;
        if ($[21] === Symbol.for("react.memo_cache_sentinel")) {
            t12 = ({
                "DepartamentsPage[<button>.onClick]": ()=>setPage(_DepartamentsPageButtonOnClickSetPage)
            })["DepartamentsPage[<button>.onClick]"];
            $[21] = t12;
        } else {
            t12 = $[21];
        }
        const t13 = page <= 1 || isLoading;
        if ($[22] !== t13) {
            t5 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("button", {
                type: "button",
                className: "rounded-md border border-border bg-muted px-3 py-1.5 text-xs font-semibold text-foreground transition hover:border-primary hover:text-primary disabled:opacity-50 disabled:cursor-not-allowed",
                onClick: t12,
                disabled: t13,
                children: "← Prev"
            }, void 0, false, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 111,
                columnNumber: 12
            }, this);
            $[22] = t13;
            $[23] = t5;
        } else {
            t5 = $[23];
        }
        let t14;
        if ($[24] !== isLoading || $[25] !== page) {
            t14 = ({
                "DepartamentsPage[pagerPages.map()]": (item, index)=>item === "..." ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                        className: "px-2 text-xs text-muted-foreground",
                        children: "..."
                    }, `dots-${index}`, false, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 120,
                        columnNumber: 81
                    }, this) : /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("button", {
                        type: "button",
                        onClick: {
                            "DepartamentsPage[pagerPages.map() > <button>.onClick]": ()=>setPage(Number(item))
                        }["DepartamentsPage[pagerPages.map() > <button>.onClick]"],
                        disabled: isLoading,
                        className: `rounded-md border px-2 py-1 text-xs font-semibold ${item === page ? "border-primary bg-primary text-white" : "border-border bg-background text-foreground hover:border-primary"}`,
                        children: item
                    }, `page-${item}`, false, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 120,
                        columnNumber: 169
                    }, this)
            })["DepartamentsPage[pagerPages.map()]"];
            $[24] = isLoading;
            $[25] = page;
            $[26] = t14;
        } else {
            t14 = $[26];
        }
        t6 = pagerPages.map(t14);
        $[7] = isLoading;
        $[8] = lastPage;
        $[9] = page;
        $[10] = total;
        $[11] = t10;
        $[12] = t4;
        $[13] = t5;
        $[14] = t6;
        $[15] = t7;
        $[16] = t8;
        $[17] = t9;
    } else {
        t10 = $[11];
        t4 = $[12];
        t5 = $[13];
        t6 = $[14];
        t7 = $[15];
        t8 = $[16];
        t9 = $[17];
    }
    let t11;
    if ($[27] !== lastPage) {
        t11 = ({
            "DepartamentsPage[<button>.onClick]": ()=>setPage({
                    "DepartamentsPage[<button>.onClick > setPage()]": (prev_0)=>Math.min(lastPage, prev_0 + 1)
                }["DepartamentsPage[<button>.onClick > setPage()]"])
        })["DepartamentsPage[<button>.onClick]"];
        $[27] = lastPage;
        $[28] = t11;
    } else {
        t11 = $[28];
    }
    const t12 = page >= lastPage || isLoading;
    let t13;
    if ($[29] !== t11 || $[30] !== t12) {
        t13 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("button", {
            type: "button",
            className: "rounded-md border border-border bg-muted px-3 py-1.5 text-xs font-semibold text-foreground transition hover:border-primary hover:text-primary disabled:opacity-50 disabled:cursor-not-allowed",
            onClick: t11,
            disabled: t12,
            children: "Next →"
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 166,
            columnNumber: 11
        }, this);
        $[29] = t11;
        $[30] = t12;
        $[31] = t13;
    } else {
        t13 = $[31];
    }
    let t14;
    if ($[32] !== lastPage || $[33] !== page) {
        t14 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
            className: "ml-auto text-xs text-muted-foreground",
            children: [
                "Page ",
                page,
                " of ",
                lastPage
            ]
        }, void 0, true, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 175,
            columnNumber: 11
        }, this);
        $[32] = lastPage;
        $[33] = page;
        $[34] = t14;
    } else {
        t14 = $[34];
    }
    let t15;
    if ($[35] !== t13 || $[36] !== t14 || $[37] !== t4 || $[38] !== t5 || $[39] !== t6) {
        t15 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: t4,
            children: [
                t5,
                t6,
                t13,
                t14
            ]
        }, void 0, true, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 184,
            columnNumber: 11
        }, this);
        $[35] = t13;
        $[36] = t14;
        $[37] = t4;
        $[38] = t5;
        $[39] = t6;
        $[40] = t15;
    } else {
        t15 = $[40];
    }
    let t16;
    if ($[41] !== t15 || $[42] !== t7 || $[43] !== t8) {
        t16 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("section", {
            className: t7,
            children: [
                t8,
                t15
            ]
        }, void 0, true, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 196,
            columnNumber: 11
        }, this);
        $[41] = t15;
        $[42] = t7;
        $[43] = t8;
        $[44] = t16;
    } else {
        t16 = $[44];
    }
    let t17;
    if ($[45] !== error || $[46] !== isLoading || $[47] !== models) {
        t17 = isLoading ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-border bg-card p-4 text-center text-muted-foreground",
            children: "Loading..."
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 206,
            columnNumber: 23
        }, this) : error ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-destructive/40 bg-destructive/10 p-4 text-center text-destructive",
            children: "Error loading departments. Please refresh."
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 206,
            columnNumber: 146
        }, this) : models.length === 0 ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-border bg-card p-4 text-center text-muted-foreground",
            children: "No departments found."
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 206,
            columnNumber: 328
        }, this) : /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "grid gap-3 sm:grid-cols-2 xl:grid-cols-3",
            children: models.map(_DepartamentsPageModelsMap)
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 206,
            columnNumber: 454
        }, this);
        $[45] = error;
        $[46] = isLoading;
        $[47] = models;
        $[48] = t17;
    } else {
        t17 = $[48];
    }
    let t18;
    if ($[49] !== t16 || $[50] !== t17 || $[51] !== t9) {
        t18 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: t9,
            children: [
                t16,
                t17
            ]
        }, void 0, true, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 216,
            columnNumber: 11
        }, this);
        $[49] = t16;
        $[50] = t17;
        $[51] = t9;
        $[52] = t18;
    } else {
        t18 = $[52];
    }
    let t19;
    if ($[53] !== t10 || $[54] !== t18) {
        t19 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: t10,
            children: t18
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 226,
            columnNumber: 11
        }, this);
        $[53] = t10;
        $[54] = t18;
        $[55] = t19;
    } else {
        t19 = $[55];
    }
    return t19;
}
_s(DepartamentsPage, "FGrRqmq1uMtqK0CueYsz+lwqP/Y=", false, function() {
    return [
        __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$tanstack$2f$react$2d$query$2f$build$2f$modern$2f$useQuery$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useQuery"]
    ];
});
_c = DepartamentsPage;
function _DepartamentsPageModelsMap(item_0) {
    return /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("article", {
        className: "flex min-h-[200px] flex-col justify-between rounded-2xl border border-border bg-card p-3 sm:p-4 transition hover:-translate-y-0.5 hover:shadow-md",
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
                                        className: "text-base font-semibold sm:text-lg truncate",
                                        children: item_0.name
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 236,
                                        columnNumber: 311
                                    }, this),
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                                        className: "mt-0.5 text-xs text-muted-foreground sm:text-sm truncate",
                                        children: [
                                            item_0.identifier,
                                            " · ",
                                            item_0.path
                                        ]
                                    }, void 0, true, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 236,
                                        columnNumber: 389
                                    }, this)
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 236,
                                columnNumber: 286
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                className: `rounded-full px-2 py-0.5 text-[10px] font-semibold sm:text-xs ${item_0.isActive ? "bg-emerald-500/10 text-emerald-600" : "bg-red-500/10 text-red-600"}`,
                                children: item_0.isActive ? "Active" : "Inactive"
                            }, void 0, false, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 236,
                                columnNumber: 506
                            }, this)
                        ]
                    }, void 0, true, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 236,
                        columnNumber: 220
                    }, this),
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                        className: "grid gap-2 text-xs text-muted-foreground sm:grid-cols-2 sm:text-sm",
                        children: [
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "rounded-md bg-muted/60 p-2",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                        className: "font-medium text-foreground",
                                        children: "Depth:"
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 236,
                                        columnNumber: 859
                                    }, this),
                                    " ",
                                    item_0.depth
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 236,
                                columnNumber: 815
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "rounded-md bg-muted/60 p-2",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                        className: "font-medium text-foreground",
                                        children: "Parent:"
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 236,
                                        columnNumber: 983
                                    }, this),
                                    " ",
                                    item_0.parentId ?? "\u2014"
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 236,
                                columnNumber: 939
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "rounded-md bg-muted/60 p-2",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                        className: "font-medium text-foreground",
                                        children: "Children:"
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 236,
                                        columnNumber: 1123
                                    }, this),
                                    " ",
                                    item_0.childs?.length ?? 0
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 236,
                                columnNumber: 1079
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "rounded-md bg-muted/60 p-2",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                        className: "font-medium text-foreground",
                                        children: "More:"
                                    }, void 0, false, {
                                        fileName: "[project]/src/app/departaments/page.tsx",
                                        lineNumber: 236,
                                        columnNumber: 1264
                                    }, this),
                                    " ",
                                    item_0.hasMoreChildren ? "Yes" : "No"
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 236,
                                columnNumber: 1220
                            }, this)
                        ]
                    }, void 0, true, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 236,
                        columnNumber: 731
                    }, this)
                ]
            }, void 0, true, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 236,
                columnNumber: 193
            }, this),
            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "mt-2 rounded-xl border border-border p-2 text-xs text-muted-foreground sm:text-sm",
                children: item_0.childs && item_0.childs.length > 0 ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["Fragment"], {
                    children: [
                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                            className: "font-medium text-foreground",
                            children: "Child departments:"
                        }, void 0, false, {
                            fileName: "[project]/src/app/departaments/page.tsx",
                            lineNumber: 236,
                            columnNumber: 1526
                        }, this),
                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("ul", {
                            className: "ml-3 mt-1 list-disc space-y-1",
                            children: [
                                item_0.childs.slice(0, 3).map(_DepartamentsPageModelsMapAnonymous),
                                item_0.childs.length > 3 ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("li", {
                                    children: [
                                        "+",
                                        item_0.childs.length - 3,
                                        " more..."
                                    ]
                                }, void 0, true, {
                                    fileName: "[project]/src/app/departaments/page.tsx",
                                    lineNumber: 236,
                                    columnNumber: 1737
                                }, this) : null
                            ]
                        }, void 0, true, {
                            fileName: "[project]/src/app/departaments/page.tsx",
                            lineNumber: 236,
                            columnNumber: 1595
                        }, this)
                    ]
                }, void 0, true) : /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                    children: "No child departments"
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 236,
                    columnNumber: 1800
                }, this)
            }, void 0, false, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 236,
                columnNumber: 1380
            }, this)
        ]
    }, item_0.id, true, {
        fileName: "[project]/src/app/departaments/page.tsx",
        lineNumber: 236,
        columnNumber: 10
    }, this);
}
function _DepartamentsPageModelsMapAnonymous(child) {
    return /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("li", {
        className: "truncate",
        children: child.name
    }, child.id, false, {
        fileName: "[project]/src/app/departaments/page.tsx",
        lineNumber: 239,
        columnNumber: 10
    }, this);
}
function _DepartamentsPageButtonOnClickSetPage(prev) {
    return Math.max(1, prev - 1);
}
var _c;
__turbopack_context__.k.register(_c, "DepartamentsPage");
if (typeof globalThis.$RefreshHelpers$ === 'object' && globalThis.$RefreshHelpers !== null) {
    __turbopack_context__.k.registerExports(__turbopack_context__.m, globalThis.$RefreshHelpers$);
}
}),
]);

//# sourceMappingURL=src_bd3f4259._.js.map