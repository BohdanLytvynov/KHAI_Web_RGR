import { Routes } from '@angular/router';

import { StartPageComponent } from './components/start-page/start-page.component';
import { MainPageComponent } from './components/main-page/main-page.component';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';
import { RegFailComponent } from './components/reg-fail/reg-fail/reg-fail.component';


export const routes: Routes = [
  {path: "", component: StartPageComponent},
  {path: "main", component: MainPageComponent},
  {path: "profile", component: ProfilePageComponent},
  {path: "reg-fail", component: RegFailComponent},
  {path: "start", component: StartPageComponent},
  {path: "**", redirectTo: "/"}  
];
