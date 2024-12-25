import { Component } from '@angular/core';
import { LogInComponent } from "../log-in/log-in.component";
import { SignUpComponent } from "../sign-up/sign-up.component";

@Component({
  selector: 'app-start-page',
  standalone: true,
  imports: [LogInComponent, SignUpComponent],
  templateUrl: './start-page.component.html',
  styleUrl: './start-page.component.css'
})
export class StartPageComponent {

  showLogIn = false;
  showSignUp = false;

  login = false;
  signup = false;

  showLogInComponent() {
    this.login = true;
    this.showLogIn = true;
  };

  hideLogInComponent(value: boolean) {
    this.showLogIn = value;
  };

  showSignUpComponent() {
    this.signup = true;
    this.showSignUp = true;
  }

  hideSignUpComponent(value: boolean) {
    this.showSignUp = value;
  }

}
