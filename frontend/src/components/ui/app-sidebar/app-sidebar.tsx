import { Sidebar, SidebarContent, SidebarFooter, SidebarGroup, SidebarHeader, SidebarMenu, SidebarMenuItem, SidebarMenuButton } from "../sidebar";
import Link from "next/link";
import { routes } from "@/shared/routes";

export default function AppSidebar(){
    return <Sidebar>
            <SidebarHeader>DS App</SidebarHeader>
            <SidebarContent>
                <SidebarGroup>
                    <SidebarMenu>
                        <SidebarMenuItem>
                            <Link href={routes.home}>
                                <SidebarMenuButton>Главная</SidebarMenuButton>
                            </Link>
                        </SidebarMenuItem>
                        <SidebarMenuItem>
                            <Link href={routes.sklad}>
                                <SidebarMenuButton>Склад</SidebarMenuButton>
                            </Link>
                        </SidebarMenuItem>
                        <SidebarMenuItem>
                            <Link href={routes.locomar}>
                                <SidebarMenuButton>Локомар</SidebarMenuButton>
                            </Link>
                        </SidebarMenuItem>
                        <SidebarMenuItem>
                            <Link href={routes.departaments}>
                                <SidebarMenuButton>Департаменты</SidebarMenuButton>
                            </Link>
                        </SidebarMenuItem>
                    </SidebarMenu>
                </SidebarGroup>
            </SidebarContent>
            <SidebarFooter>© 2025</SidebarFooter>
    </Sidebar>
}