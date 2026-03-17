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
"[project]/src/shared/components/ui/pagination.tsx [app-client] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "Pagination",
    ()=>Pagination,
    "PaginationContent",
    ()=>PaginationContent,
    "PaginationEllipsis",
    ()=>PaginationEllipsis,
    "PaginationItem",
    ()=>PaginationItem,
    "PaginationLink",
    ()=>PaginationLink,
    "PaginationNext",
    ()=>PaginationNext,
    "PaginationPrevious",
    ()=>PaginationPrevious
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/jsx-dev-runtime.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/compiler-runtime.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$lib$2f$utils$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/src/lib/utils.ts [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$components$2f$ui$2f$button$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/src/shared/components/ui/button.tsx [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$react$2f$dist$2f$esm$2f$index$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__$3c$locals$3e$__ = __turbopack_context__.i("[project]/node_modules/@hugeicons/react/dist/esm/index.js [app-client] (ecmascript) <locals>");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$react$2f$dist$2f$esm$2f$HugeiconsIcon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/@hugeicons/react/dist/esm/HugeiconsIcon.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$core$2d$free$2d$icons$2f$dist$2f$esm$2f$ArrowLeft01Icon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__$3c$export__default__as__ArrowLeft01Icon$3e$__ = __turbopack_context__.i("[project]/node_modules/@hugeicons/core-free-icons/dist/esm/ArrowLeft01Icon.js [app-client] (ecmascript) <export default as ArrowLeft01Icon>");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$core$2d$free$2d$icons$2f$dist$2f$esm$2f$ArrowRight01Icon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__$3c$export__default__as__ArrowRight01Icon$3e$__ = __turbopack_context__.i("[project]/node_modules/@hugeicons/core-free-icons/dist/esm/ArrowRight01Icon.js [app-client] (ecmascript) <export default as ArrowRight01Icon>");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$core$2d$free$2d$icons$2f$dist$2f$esm$2f$MoreHorizontalCircle01Icon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__$3c$export__default__as__MoreHorizontalCircle01Icon$3e$__ = __turbopack_context__.i("[project]/node_modules/@hugeicons/core-free-icons/dist/esm/MoreHorizontalCircle01Icon.js [app-client] (ecmascript) <export default as MoreHorizontalCircle01Icon>");
;
;
;
;
;
;
function Pagination(t0) {
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(9);
    if ($[0] !== "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099") {
        for(let $i = 0; $i < 9; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099";
    }
    let className;
    let props;
    if ($[1] !== t0) {
        ({ className, ...props } = t0);
        $[1] = t0;
        $[2] = className;
        $[3] = props;
    } else {
        className = $[2];
        props = $[3];
    }
    let t1;
    if ($[4] !== className) {
        t1 = (0, __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$lib$2f$utils$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["cn"])("mx-auto flex w-full justify-center", className);
        $[4] = className;
        $[5] = t1;
    } else {
        t1 = $[5];
    }
    let t2;
    if ($[6] !== props || $[7] !== t1) {
        t2 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("nav", {
            role: "navigation",
            "aria-label": "pagination",
            "data-slot": "pagination",
            className: t1,
            ...props
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 39,
            columnNumber: 10
        }, this);
        $[6] = props;
        $[7] = t1;
        $[8] = t2;
    } else {
        t2 = $[8];
    }
    return t2;
}
_c = Pagination;
function PaginationContent(t0) {
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(9);
    if ($[0] !== "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099") {
        for(let $i = 0; $i < 9; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099";
    }
    let className;
    let props;
    if ($[1] !== t0) {
        ({ className, ...props } = t0);
        $[1] = t0;
        $[2] = className;
        $[3] = props;
    } else {
        className = $[2];
        props = $[3];
    }
    let t1;
    if ($[4] !== className) {
        t1 = (0, __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$lib$2f$utils$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["cn"])("flex items-center gap-0.5", className);
        $[4] = className;
        $[5] = t1;
    } else {
        t1 = $[5];
    }
    let t2;
    if ($[6] !== props || $[7] !== t1) {
        t2 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("ul", {
            "data-slot": "pagination-content",
            className: t1,
            ...props
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 80,
            columnNumber: 10
        }, this);
        $[6] = props;
        $[7] = t1;
        $[8] = t2;
    } else {
        t2 = $[8];
    }
    return t2;
}
_c1 = PaginationContent;
function PaginationItem(t0) {
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(5);
    if ($[0] !== "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099") {
        for(let $i = 0; $i < 5; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099";
    }
    let props;
    if ($[1] !== t0) {
        ({ ...props } = t0);
        $[1] = t0;
        $[2] = props;
    } else {
        props = $[2];
    }
    let t1;
    if ($[3] !== props) {
        t1 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("li", {
            "data-slot": "pagination-item",
            ...props
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 109,
            columnNumber: 10
        }, this);
        $[3] = props;
        $[4] = t1;
    } else {
        t1 = $[4];
    }
    return t1;
}
_c2 = PaginationItem;
function PaginationLink(t0) {
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(17);
    if ($[0] !== "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099") {
        for(let $i = 0; $i < 17; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099";
    }
    let className;
    let isActive;
    let props;
    let t1;
    if ($[1] !== t0) {
        ({ className, isActive, size: t1, ...props } = t0);
        $[1] = t0;
        $[2] = className;
        $[3] = isActive;
        $[4] = props;
        $[5] = t1;
    } else {
        className = $[2];
        isActive = $[3];
        props = $[4];
        t1 = $[5];
    }
    const size = t1 === undefined ? "icon" : t1;
    const t2 = isActive ? "outline" : "ghost";
    let t3;
    if ($[6] !== className) {
        t3 = (0, __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$lib$2f$utils$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["cn"])(className);
        $[6] = className;
        $[7] = t3;
    } else {
        t3 = $[7];
    }
    const t4 = isActive ? "page" : undefined;
    let t5;
    if ($[8] !== isActive || $[9] !== props || $[10] !== t4) {
        t5 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("a", {
            "aria-current": t4,
            "data-slot": "pagination-link",
            "data-active": isActive,
            ...props
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 163,
            columnNumber: 10
        }, this);
        $[8] = isActive;
        $[9] = props;
        $[10] = t4;
        $[11] = t5;
    } else {
        t5 = $[11];
    }
    let t6;
    if ($[12] !== size || $[13] !== t2 || $[14] !== t3 || $[15] !== t5) {
        t6 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$components$2f$ui$2f$button$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__["Button"], {
            variant: t2,
            size: size,
            className: t3,
            nativeButton: false,
            render: t5
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 173,
            columnNumber: 10
        }, this);
        $[12] = size;
        $[13] = t2;
        $[14] = t3;
        $[15] = t5;
        $[16] = t6;
    } else {
        t6 = $[16];
    }
    return t6;
}
_c3 = PaginationLink;
function PaginationPrevious(t0) {
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(14);
    if ($[0] !== "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099") {
        for(let $i = 0; $i < 14; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099";
    }
    let className;
    let props;
    let t1;
    if ($[1] !== t0) {
        ({ className, text: t1, ...props } = t0);
        $[1] = t0;
        $[2] = className;
        $[3] = props;
        $[4] = t1;
    } else {
        className = $[2];
        props = $[3];
        t1 = $[4];
    }
    const text = t1 === undefined ? "Previous" : t1;
    let t2;
    if ($[5] !== className) {
        t2 = (0, __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$lib$2f$utils$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["cn"])("pl-2!", className);
        $[5] = className;
        $[6] = t2;
    } else {
        t2 = $[6];
    }
    let t3;
    if ($[7] === Symbol.for("react.memo_cache_sentinel")) {
        t3 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$react$2f$dist$2f$esm$2f$HugeiconsIcon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["HugeiconsIcon"], {
            icon: __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$core$2d$free$2d$icons$2f$dist$2f$esm$2f$ArrowLeft01Icon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__$3c$export__default__as__ArrowLeft01Icon$3e$__["ArrowLeft01Icon"],
            strokeWidth: 2,
            "data-icon": "inline-start"
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 221,
            columnNumber: 10
        }, this);
        $[7] = t3;
    } else {
        t3 = $[7];
    }
    let t4;
    if ($[8] !== text) {
        t4 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
            className: "hidden sm:block",
            children: text
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 228,
            columnNumber: 10
        }, this);
        $[8] = text;
        $[9] = t4;
    } else {
        t4 = $[9];
    }
    let t5;
    if ($[10] !== props || $[11] !== t2 || $[12] !== t4) {
        t5 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(PaginationLink, {
            "aria-label": "Go to previous page",
            size: "default",
            className: t2,
            ...props,
            children: [
                t3,
                t4
            ]
        }, void 0, true, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 236,
            columnNumber: 10
        }, this);
        $[10] = props;
        $[11] = t2;
        $[12] = t4;
        $[13] = t5;
    } else {
        t5 = $[13];
    }
    return t5;
}
_c4 = PaginationPrevious;
function PaginationNext(t0) {
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(14);
    if ($[0] !== "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099") {
        for(let $i = 0; $i < 14; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099";
    }
    let className;
    let props;
    let t1;
    if ($[1] !== t0) {
        ({ className, text: t1, ...props } = t0);
        $[1] = t0;
        $[2] = className;
        $[3] = props;
        $[4] = t1;
    } else {
        className = $[2];
        props = $[3];
        t1 = $[4];
    }
    const text = t1 === undefined ? "Next" : t1;
    let t2;
    if ($[5] !== className) {
        t2 = (0, __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$lib$2f$utils$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["cn"])("pr-2!", className);
        $[5] = className;
        $[6] = t2;
    } else {
        t2 = $[6];
    }
    let t3;
    if ($[7] !== text) {
        t3 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
            className: "hidden sm:block",
            children: text
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 283,
            columnNumber: 10
        }, this);
        $[7] = text;
        $[8] = t3;
    } else {
        t3 = $[8];
    }
    let t4;
    if ($[9] === Symbol.for("react.memo_cache_sentinel")) {
        t4 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$react$2f$dist$2f$esm$2f$HugeiconsIcon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["HugeiconsIcon"], {
            icon: __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$core$2d$free$2d$icons$2f$dist$2f$esm$2f$ArrowRight01Icon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__$3c$export__default__as__ArrowRight01Icon$3e$__["ArrowRight01Icon"],
            strokeWidth: 2,
            "data-icon": "inline-end"
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 291,
            columnNumber: 10
        }, this);
        $[9] = t4;
    } else {
        t4 = $[9];
    }
    let t5;
    if ($[10] !== props || $[11] !== t2 || $[12] !== t3) {
        t5 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(PaginationLink, {
            "aria-label": "Go to next page",
            size: "default",
            className: t2,
            ...props,
            children: [
                t3,
                t4
            ]
        }, void 0, true, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 298,
            columnNumber: 10
        }, this);
        $[10] = props;
        $[11] = t2;
        $[12] = t3;
        $[13] = t5;
    } else {
        t5 = $[13];
    }
    return t5;
}
_c5 = PaginationNext;
function PaginationEllipsis(t0) {
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(11);
    if ($[0] !== "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099") {
        for(let $i = 0; $i < 11; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "1ab56179fda77769224ebf586169a13d9e6d78e319a3ccba0a39085b9d84c099";
    }
    let className;
    let props;
    if ($[1] !== t0) {
        ({ className, ...props } = t0);
        $[1] = t0;
        $[2] = className;
        $[3] = props;
    } else {
        className = $[2];
        props = $[3];
    }
    let t1;
    if ($[4] !== className) {
        t1 = (0, __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$lib$2f$utils$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["cn"])("flex size-7 items-center justify-center [&_svg:not([class*='size-'])]:size-3.5", className);
        $[4] = className;
        $[5] = t1;
    } else {
        t1 = $[5];
    }
    let t2;
    let t3;
    if ($[6] === Symbol.for("react.memo_cache_sentinel")) {
        t2 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$react$2f$dist$2f$esm$2f$HugeiconsIcon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["HugeiconsIcon"], {
            icon: __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$hugeicons$2f$core$2d$free$2d$icons$2f$dist$2f$esm$2f$MoreHorizontalCircle01Icon$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__$3c$export__default__as__MoreHorizontalCircle01Icon$3e$__["MoreHorizontalCircle01Icon"],
            strokeWidth: 2
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 341,
            columnNumber: 10
        }, this);
        t3 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
            className: "sr-only",
            children: "More pages"
        }, void 0, false, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 342,
            columnNumber: 10
        }, this);
        $[6] = t2;
        $[7] = t3;
    } else {
        t2 = $[6];
        t3 = $[7];
    }
    let t4;
    if ($[8] !== props || $[9] !== t1) {
        t4 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
            "aria-hidden": true,
            "data-slot": "pagination-ellipsis",
            className: t1,
            ...props,
            children: [
                t2,
                t3
            ]
        }, void 0, true, {
            fileName: "[project]/src/shared/components/ui/pagination.tsx",
            lineNumber: 351,
            columnNumber: 10
        }, this);
        $[8] = props;
        $[9] = t1;
        $[10] = t4;
    } else {
        t4 = $[10];
    }
    return t4;
}
_c6 = PaginationEllipsis;
;
var _c, _c1, _c2, _c3, _c4, _c5, _c6;
__turbopack_context__.k.register(_c, "Pagination");
__turbopack_context__.k.register(_c1, "PaginationContent");
__turbopack_context__.k.register(_c2, "PaginationItem");
__turbopack_context__.k.register(_c3, "PaginationLink");
__turbopack_context__.k.register(_c4, "PaginationPrevious");
__turbopack_context__.k.register(_c5, "PaginationNext");
__turbopack_context__.k.register(_c6, "PaginationEllipsis");
if (typeof globalThis.$RefreshHelpers$ === 'object' && globalThis.$RefreshHelpers !== null) {
    __turbopack_context__.k.registerExports(__turbopack_context__.m, globalThis.$RefreshHelpers$);
}
}),
"[project]/src/features/departament/departaments-pagination.tsx [app-client] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "DepartamentsPagination",
    ()=>DepartamentsPagination
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/jsx-dev-runtime.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/compiler-runtime.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$components$2f$ui$2f$pagination$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/src/shared/components/ui/pagination.tsx [app-client] (ecmascript)");
"use client";
;
;
;
function DepartamentsPagination(t0) {
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(16);
    if ($[0] !== "daba0ff3cb4fa1f280700155529e64c749ce1bfd798682b9a3fc4a1f33578424") {
        for(let $i = 0; $i < 16; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "daba0ff3cb4fa1f280700155529e64c749ce1bfd798682b9a3fc4a1f33578424";
    }
    const { page, setPage, total, pageSize } = t0;
    const lastPage = Math.max(1, Math.ceil(total / pageSize));
    let t1;
    if ($[1] !== lastPage) {
        t1 = Array.from({
            length: lastPage
        }, _DepartamentsPaginationArrayFrom);
        $[1] = lastPage;
        $[2] = t1;
    } else {
        t1 = $[2];
    }
    const pages = t1;
    let t2;
    if ($[3] !== setPage) {
        t2 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$components$2f$ui$2f$pagination$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__["PaginationPrevious"], {
            onClick: {
                "DepartamentsPagination[<PaginationPrevious>.onClick]": ()=>setPage(_DepartamentsPaginationPaginationPreviousOnClickSetPage)
            }["DepartamentsPagination[<PaginationPrevious>.onClick]"],
            text: "\u041D\u0430\u0437\u0430\u0434"
        }, void 0, false, {
            fileName: "[project]/src/features/departament/departaments-pagination.tsx",
            lineNumber: 39,
            columnNumber: 10
        }, this);
        $[3] = setPage;
        $[4] = t2;
    } else {
        t2 = $[4];
    }
    let t3;
    if ($[5] !== page || $[6] !== pages || $[7] !== setPage) {
        t3 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$components$2f$ui$2f$pagination$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__["PaginationContent"], {
            children: pages.map({
                "DepartamentsPagination[pages.map()]": (p)=>/*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$components$2f$ui$2f$pagination$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__["PaginationItem"], {
                        children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$components$2f$ui$2f$pagination$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__["PaginationLink"], {
                            href: "#",
                            isActive: p === page,
                            onClick: {
                                "DepartamentsPagination[pages.map() > <PaginationLink>.onClick]": (event)=>{
                                    event.preventDefault();
                                    setPage(p);
                                }
                            }["DepartamentsPagination[pages.map() > <PaginationLink>.onClick]"],
                            children: p
                        }, void 0, false, {
                            fileName: "[project]/src/features/departament/departaments-pagination.tsx",
                            lineNumber: 50,
                            columnNumber: 77
                        }, this)
                    }, p, false, {
                        fileName: "[project]/src/features/departament/departaments-pagination.tsx",
                        lineNumber: 50,
                        columnNumber: 53
                    }, this)
            }["DepartamentsPagination[pages.map()]"])
        }, void 0, false, {
            fileName: "[project]/src/features/departament/departaments-pagination.tsx",
            lineNumber: 49,
            columnNumber: 10
        }, this);
        $[5] = page;
        $[6] = pages;
        $[7] = setPage;
        $[8] = t3;
    } else {
        t3 = $[8];
    }
    let t4;
    if ($[9] !== lastPage || $[10] !== setPage) {
        t4 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$components$2f$ui$2f$pagination$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__["PaginationNext"], {
            onClick: {
                "DepartamentsPagination[<PaginationNext>.onClick]": ()=>setPage({
                        "DepartamentsPagination[<PaginationNext>.onClick > setPage()]": (prev_0)=>Math.min(lastPage, prev_0 + 1)
                    }["DepartamentsPagination[<PaginationNext>.onClick > setPage()]"])
            }["DepartamentsPagination[<PaginationNext>.onClick]"],
            text: "\u0414\u0430\u043B\u0435\u0435"
        }, void 0, false, {
            fileName: "[project]/src/features/departament/departaments-pagination.tsx",
            lineNumber: 66,
            columnNumber: 10
        }, this);
        $[9] = lastPage;
        $[10] = setPage;
        $[11] = t4;
    } else {
        t4 = $[11];
    }
    let t5;
    if ($[12] !== t2 || $[13] !== t3 || $[14] !== t4) {
        t5 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$shared$2f$components$2f$ui$2f$pagination$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__["Pagination"], {
            className: "mt-4",
            children: [
                t2,
                t3,
                t4
            ]
        }, void 0, true, {
            fileName: "[project]/src/features/departament/departaments-pagination.tsx",
            lineNumber: 79,
            columnNumber: 10
        }, this);
        $[12] = t2;
        $[13] = t3;
        $[14] = t4;
        $[15] = t5;
    } else {
        t5 = $[15];
    }
    return t5;
}
_c = DepartamentsPagination;
function _DepartamentsPaginationPaginationPreviousOnClickSetPage(prev) {
    return Math.max(1, prev - 1);
}
function _DepartamentsPaginationArrayFrom(_v, i) {
    return i + 1;
}
var _c;
__turbopack_context__.k.register(_c, "DepartamentsPagination");
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
var __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$features$2f$departament$2f$departaments$2d$pagination$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/src/features/departament/departaments-pagination.tsx [app-client] (ecmascript)");
;
var _s = __turbopack_context__.k.signature();
"use client";
;
;
;
;
;
const PAGE_SIZE = 5;
function DepartamentsPage() {
    _s();
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(24);
    if ($[0] !== "020dd52f510a9ca0c5587a5ead6886843cfa8f726f5dadf60337f9069e60be20") {
        for(let $i = 0; $i < 24; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "020dd52f510a9ca0c5587a5ead6886843cfa8f726f5dadf60337f9069e60be20";
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
    Array.from({
        length: lastPage
    }, _DepartamentsPageArrayFrom);
    let t2;
    if ($[5] !== allModels || $[6] !== page) {
        t2 = allModels.length > PAGE_SIZE ? allModels.slice((page - 1) * PAGE_SIZE, page * PAGE_SIZE) : allModels;
        $[5] = allModels;
        $[6] = page;
        $[7] = t2;
    } else {
        t2 = $[7];
    }
    const models = t2;
    let t3;
    let t4;
    if ($[8] !== lastPage || $[9] !== page) {
        t3 = ({
            "DepartamentsPage[useEffect()]": ()=>{
                if (page > lastPage) {
                    setPage(lastPage);
                }
                if (page < 1) {
                    setPage(1);
                }
            }
        })["DepartamentsPage[useEffect()]"];
        t4 = [
            page,
            lastPage
        ];
        $[8] = lastPage;
        $[9] = page;
        $[10] = t3;
        $[11] = t4;
    } else {
        t3 = $[10];
        t4 = $[11];
    }
    (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$index$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useEffect"])(t3, t4);
    let t5;
    if ($[12] === Symbol.for("react.memo_cache_sentinel")) {
        t5 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            children: [
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                    className: "text-xs uppercase tracking-[0.25em] text-muted-foreground",
                    children: "Локации / Департаменты"
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 89,
                    columnNumber: 15
                }, this),
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("h1", {
                    className: "mt-1 text-2xl font-bold sm:text-3xl",
                    children: "Департаменты"
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 89,
                    columnNumber: 114
                }, this),
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                    className: "mt-1 max-w-3xl text-sm text-muted-foreground sm:text-base",
                    children: "Список корневых департаментов. Карточки адаптируются под экран и длинный текст."
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 89,
                    columnNumber: 183
                }, this)
            ]
        }, void 0, true, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 89,
            columnNumber: 10
        }, this);
        $[12] = t5;
    } else {
        t5 = $[12];
    }
    let t6;
    if ($[13] !== total) {
        t6 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-border bg-card p-4 sm:p-5",
            children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "flex flex-wrap items-start justify-between gap-3",
                children: [
                    t5,
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                        className: "text-xs sm:text-sm",
                        children: [
                            "Всего департаментов: ",
                            total
                        ]
                    }, void 0, true, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 96,
                        columnNumber: 149
                    }, this)
                ]
            }, void 0, true, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 96,
                columnNumber: 79
            }, this)
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 96,
            columnNumber: 10
        }, this);
        $[13] = total;
        $[14] = t6;
    } else {
        t6 = $[14];
    }
    let t7;
    if ($[15] !== error || $[16] !== isLoading || $[17] !== models || $[18] !== page || $[19] !== total) {
        t7 = isLoading ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-border bg-card p-4 text-center text-muted-foreground",
            children: "Загрузка департаментов..."
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 104,
            columnNumber: 22
        }, this) : error ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-destructive/40 bg-destructive/10 p-4 text-center text-destructive",
            children: "Ошибка загрузки. Обновите страницу."
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 104,
            columnNumber: 160
        }, this) : models.length === 0 ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "rounded-2xl border border-border bg-card p-4 text-center text-muted-foreground",
            children: "Департаменты не найдены."
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 104,
            columnNumber: 335
        }, this) : /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["Fragment"], {
            children: [
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                    className: "grid gap-3 sm:grid-cols-2 xl:grid-cols-3",
                    children: models.map(_DepartamentsPageModelsMap)
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 104,
                    columnNumber: 466
                }, this),
                /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$src$2f$features$2f$departament$2f$departaments$2d$pagination$2e$tsx__$5b$app$2d$client$5d$__$28$ecmascript$29$__["DepartamentsPagination"], {
                    page: page,
                    setPage: setPage,
                    total: total,
                    pageSize: PAGE_SIZE
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 104,
                    columnNumber: 570
                }, this)
            ]
        }, void 0, true);
        $[15] = error;
        $[16] = isLoading;
        $[17] = models;
        $[18] = page;
        $[19] = total;
        $[20] = t7;
    } else {
        t7 = $[20];
    }
    let t8;
    if ($[21] !== t6 || $[22] !== t7) {
        t8 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            className: "bg-background text-foreground min-h-screen p-3 sm:p-5 md:p-7 lg:p-8",
            children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "mx-auto max-w-6xl space-y-4",
                children: [
                    t6,
                    t7
                ]
            }, void 0, true, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 116,
                columnNumber: 95
            }, this)
        }, void 0, false, {
            fileName: "[project]/src/app/departaments/page.tsx",
            lineNumber: 116,
            columnNumber: 10
        }, this);
        $[21] = t6;
        $[22] = t7;
        $[23] = t8;
    } else {
        t8 = $[23];
    }
    return t8;
}
_s(DepartamentsPage, "DgHq2+ts1guXzJHVKhEuvQkzyJQ=", false, function() {
    return [
        __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f40$tanstack$2f$react$2d$query$2f$build$2f$modern$2f$useQuery$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useQuery"]
    ];
});
_c = DepartamentsPage;
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
                                        lineNumber: 126,
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
                                        lineNumber: 126,
                                        columnNumber: 393
                                    }, this)
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 126,
                                columnNumber: 279
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                className: `rounded-full px-2 py-0.5 text-[10px] font-semibold sm:text-xs ${item.isActive ? "bg-emerald-100 text-emerald-700" : "bg-slate-200 text-slate-600"}`,
                                children: item.isActive ? "\u0410\u043A\u0442\u0438\u0432\u0435\u043D" : "\u041D\u0435\u0430\u043A\u0442\u0438\u0432\u0435\u043D"
                            }, void 0, false, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 126,
                                columnNumber: 506
                            }, this)
                        ]
                    }, void 0, true, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 126,
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
                                        lineNumber: 126,
                                        columnNumber: 935
                                    }, this),
                                    " ",
                                    item.depth
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 126,
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
                                        lineNumber: 126,
                                        columnNumber: 1059
                                    }, this),
                                    " ",
                                    item.parentId ?? "\u2014"
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 126,
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
                                        lineNumber: 126,
                                        columnNumber: 1199
                                    }, this),
                                    " ",
                                    item.childs?.length ?? 0
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 126,
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
                                        lineNumber: 126,
                                        columnNumber: 1335
                                    }, this),
                                    " ",
                                    item.hasMoreChildren ? "\u0414\u0430" : "\u041D\u0435\u0442"
                                ]
                            }, void 0, true, {
                                fileName: "[project]/src/app/departaments/page.tsx",
                                lineNumber: 126,
                                columnNumber: 1291
                            }, this)
                        ]
                    }, void 0, true, {
                        fileName: "[project]/src/app/departaments/page.tsx",
                        lineNumber: 126,
                        columnNumber: 807
                    }, this)
                ]
            }, void 0, true, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 126,
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
                            lineNumber: 126,
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
                                    lineNumber: 126,
                                    columnNumber: 1813
                                }, this)
                            ]
                        }, void 0, true, {
                            fileName: "[project]/src/app/departaments/page.tsx",
                            lineNumber: 126,
                            columnNumber: 1674
                        }, this)
                    ]
                }, void 0, true) : /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                    children: "Нет дочерних"
                }, void 0, false, {
                    fileName: "[project]/src/app/departaments/page.tsx",
                    lineNumber: 126,
                    columnNumber: 1866
                }, this)
            }, void 0, false, {
                fileName: "[project]/src/app/departaments/page.tsx",
                lineNumber: 126,
                columnNumber: 1477
            }, this)
        ]
    }, item.id, true, {
        fileName: "[project]/src/app/departaments/page.tsx",
        lineNumber: 126,
        columnNumber: 10
    }, this);
}
function _DepartamentsPageModelsMapAnonymous(child) {
    return /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("li", {
        className: "truncate",
        children: child.name
    }, child.id, false, {
        fileName: "[project]/src/app/departaments/page.tsx",
        lineNumber: 129,
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

//# sourceMappingURL=src_5fca119f._.js.map