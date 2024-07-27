import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router'; // Import Router
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task.model';
import { TaskPriority } from '../../enums/task.priority';



@Component({
  selector: 'app-add-task',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent {
  title = '';
  description = '';
  priority: 'High' | 'Medium' | 'Low' = 'Medium';

  constructor(private taskService: TaskService, private router: Router) {} // Inject Router

  getPriorityByString(taskPriorityString:string) : TaskPriority {
    const taskPriorityLower = taskPriorityString.toLocaleLowerCase();
    switch (taskPriorityLower)
    {
        case 'low':
          return TaskPriority.Low;
        case 'medium':
          return TaskPriority.Medium;
        default:
          return TaskPriority.High;
      
    }
  }
  onSubmit() {
  
    if (this.title.trim() === '' ) {
      return; // Prevent submission if validation fails
    }

    const taskPriority = this.getPriorityByString(this.priority);

    debugger
    const newTask: Task = {
      id: 0,
      title: this.title,
      description: this.description,
      priority: taskPriority,
      createdAt: new Date()
    };
    this.taskService.addTask(newTask).subscribe(task => {
      console.log('Task added:', task);
      this.router.navigate(['/tasks']); // Navigate to task list after adding
    });

    this.title = '';
    this.description = '';
    this.priority = 'Medium';
  }
}
