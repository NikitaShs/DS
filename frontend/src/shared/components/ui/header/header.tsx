import { routes } from "@/shared/routes";
import Link from "next/link";
import { SidebarTrigger } from "@/shared/components/ui/sidebar";

export default function Header(){
    return <header className="flex items-center gap-2 border-b border-border bg-card px-4 py-2 text-card-foreground">
        <SidebarTrigger />
        <Link href={routes.home}><span>DS App</span></Link>
    </header>
}
