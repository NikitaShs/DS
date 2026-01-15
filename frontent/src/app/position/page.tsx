"use client"

import { routes } from "@/Sheard/Routings"
import Link from "next/link"

export default function Hh(){
    return(
        <div><Link href={routes.homePage}><span>на главную</span></Link></div>
    )
}