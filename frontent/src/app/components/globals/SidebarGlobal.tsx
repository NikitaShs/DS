// app/components/SidebarGlobal.tsx
"use client"

import { routes } from "@/Sheard/Routings"
import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarHeader,
  SidebarProvider,
  SidebarTrigger,
  SidebarMenu,
  SidebarMenuItem,
  SidebarMenuButton
} from "@/app/components/ui/sidebar"
import Link from "next/link"
import { 
  Home, 
  Building2, 
  MapPin, 
  Briefcase,
  Menu 
} from "lucide-react"

export default function SidebarGlobal() {
  return (
    <SidebarProvider defaultOpen={true}>
      <Sidebar variant="sidebar" collapsible="offcanvas">
        <SidebarHeader className="p-4 border-b">
          <h2 className="text-lg font-semibold">Навигация</h2>
        </SidebarHeader>
        
        <SidebarContent>
          <SidebarGroup>
            <SidebarMenu>
              <SidebarMenuItem>
                <SidebarMenuButton asChild>
                  <Link 
                    href={routes.homePage} 
                    className="flex items-center gap-3 p-3"
                  >
                    <Home className="w-5 h-5" />
                    <span className="font-medium">Главная</span>
                  </Link>
                </SidebarMenuButton>
              </SidebarMenuItem>
              
              <SidebarMenuItem>
                <SidebarMenuButton asChild>
                  <Link 
                    href={routes.departamentPage} 
                    className="flex items-center gap-3 p-3"
                  >
                    <Building2 className="w-5 h-5" />
                    <span className="font-medium">Департаменты</span>
                  </Link>
                </SidebarMenuButton>
              </SidebarMenuItem>
              
              <SidebarMenuItem>
                <SidebarMenuButton asChild>
                  <Link 
                    href={routes.PositionPage} 
                    className="flex items-center gap-3 p-3"
                  >
                    <Briefcase className="w-5 h-5" />
                    <span className="font-medium">Позиции</span>
                  </Link>
                </SidebarMenuButton>
              </SidebarMenuItem>
              
              <SidebarMenuItem>
                <SidebarMenuButton asChild>
                  <Link 
                    href={routes.locationPage} 
                    className="flex items-center gap-3 p-3"
                  >
                    <MapPin className="w-5 h-5" />
                    <span className="font-medium">Локации</span>
                  </Link>
                </SidebarMenuButton>
              </SidebarMenuItem>
            </SidebarMenu>
          </SidebarGroup>
        </SidebarContent>
      </Sidebar>
      
      <SidebarTrigger className="fixed top-4 left-4 z-50 p-2 bg-white dark:bg-gray-800 border rounded-md shadow-md">
        <Menu className="w-5 h-5" />
      </SidebarTrigger>
    </SidebarProvider>
  )
}