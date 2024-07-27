import { TaskPriority } from "../enums/task.priority";

export interface Task {
    id: number;
    title: string;
    description?: string;
    priority: TaskPriority;
    createdAt: Date;
  }
  