"use client"
import { queryClient } from "@/shared/api/query-client";
import AppSidebar from "@/shared/components/ui/app-sidebar/app-sidebar";
import Header from "@/shared/components/ui/header/header";
import { SidebarInset, SidebarProvider } from "@/shared/components/ui/sidebar";
import { Toaster } from "@/shared/components/ui/sonner";
import { QueryClientProvider } from "@tanstack/react-query";

export default function Layout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
        <QueryClientProvider client={queryClient}>
        <SidebarProvider>
          <AppSidebar/>
          <SidebarInset>
            <Header/>
            <main>{children}</main>
            <Toaster position="top-center" duration={3000} richColors={true} />
          </SidebarInset>
        </SidebarProvider>
        </QueryClientProvider>
  );
}