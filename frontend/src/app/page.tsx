"use client"

import { Button } from "@/components/ui/button";
import { useState } from "react";

export default function Home() {
  const [todo, setTodo] = useState([
    {id : 1, name : "dd", active : true},
    {id : 2, name : "dad", active : true},
    {id : 3, name : "dd", active : true}
  ]);

  return (
    <div className="flex min-h-screen flex-col items-center justify-center bg-zinc-50 font-sans dark:bg-black gap-8 p-8">
      <Button 
        className="hover:bg-blue-500 dark:hover:bg-blue-600" onClick={() => setTodo([...todo, {id : 4, name : "dd", active : true}])}>
        sa
      </Button>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 w-full max-w-4xl">
        {todo.map(q => (
          <div 
            key={q.id}
          >
            <div className="space-y-2">
              <div className="flex gap-2">
                <span className="font-medium">ID:</span>
                <span>{q.id}</span>
              </div>
              <div className="flex gap-2">
                <span className="font-medium">Name:</span>
                <span>{q.name}</span>
              </div>
              <div className="flex gap-2">
                <span className="font-medium">Active:</span>
                <span>{q.active ? "✓" : "✗"}</span>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}


