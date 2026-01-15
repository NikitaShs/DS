"use client";

import { routes } from "@/Sheard/Routings";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { Building2, MapPin, Briefcase, Home, Sparkles } from "lucide-react";

export default function HeaderGlobal() {
  const pathname = usePathname();

  const navItems = [
    { href: routes.departamentPage, label: "Departments", icon: Building2 },
    { href: routes.locationPage, label: "Location", icon: MapPin },
    { href: routes.PositionPage, label: "Position", icon: Briefcase },
  ];

  return (
    <div 
      className="sticky top-0 z-50 w-full border-b bg-white/90 backdrop-blur-md"
      style={{
        borderColor: '#e5e7eb',
        boxShadow: '0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px -1px rgba(0, 0, 0, 0.1)'
      }}
    >
      <div 
        className="container mx-auto flex items-center justify-between px-6" 
        style={{ 
          height: '68px',
          flexWrap: 'nowrap',
          maxWidth: '1280px',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-between'
        }}
      >
        {/* Logo/Home Link */}
        <Link 
          href={routes.homePage} 
          className="group flex items-center gap-3 transition-all duration-300"
          style={{ 
            textDecoration: 'none',
            flexShrink: 0,
            display: 'flex',
            alignItems: 'center',
            height: '100%',
            color: '#1f2937'
          }}
        >
          <div 
            className="relative flex-shrink-0 rounded-lg transition-all duration-300 group-hover:bg-gray-100"
            style={{
              padding: '8px'
            }}
          >
            <Home 
              className="h-6 w-6 transition-transform duration-300 group-hover:scale-110 group-hover:rotate-12" 
              style={{ color: '#1f2937' }}
            />
            <Sparkles 
              className="absolute -top-0.5 -right-0.5 h-3.5 w-3.5 transition-opacity duration-300 group-hover:opacity-100" 
              style={{ 
                color: '#6366f1',
                opacity: 0
              }}
            />
          </div>
          <span 
            className="whitespace-nowrap font-bold text-xl transition-colors"
            style={{ 
              color: '#1f2937'
            }}
          >
            DS Project
          </span>
        </Link>

        {/* Navigation Links */}
        <nav 
          className="flex items-center" 
          style={{ 
            flexWrap: 'nowrap', 
            flexShrink: 0,
            display: 'flex',
            alignItems: 'center',
            height: '100%',
            gap: '8px'
          }}
        >
          {navItems.map((item) => {
            const isActive = pathname === item.href;
            const Icon = item.icon;
            
            return (
              <Link
                key={item.href}
                href={item.href}
                className={`group relative flex items-center gap-3 rounded-lg font-medium transition-all duration-200 ${
                  isActive 
                    ? 'bg-gray-100' 
                    : 'hover:bg-gray-100'
                }`}
                style={{ 
                  textDecoration: 'none',
                  whiteSpace: 'nowrap',
                  flexShrink: 0,
                  display: 'flex',
                  alignItems: 'center',
                  height: 'auto',
                  lineHeight: '1.5',
                  fontWeight: isActive ? '600' : '500',
                  fontSize: '16px',
                  padding: '12px 20px',
                  color: isActive ? '#111827' : '#374151'
                }}
              >
                <Icon 
                  className="flex-shrink-0 transition-all duration-200"
                  style={{ 
                    width: '21px',
                    height: '21px',
                    transform: 'scale(1)',
                    color: isActive ? '#111827' : '#374151',
                    display: 'block'
                  }}
                />
                <span style={{ 
                  display: 'block', 
                  color: isActive ? '#111827' : '#374151'
                }}>
                  {item.label}
                </span>
              </Link>
            );
          })}
        </nav>
      </div>
    </div>
  );
}