import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StartPageComponent } from "./components/start-page/start-page.component";
import { LogInComponent } from './components/log-in/log-in.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, StartPageComponent, LogInComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'test';
}
