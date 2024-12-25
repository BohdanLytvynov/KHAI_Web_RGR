import { Component, ElementRef, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { CommonModule, NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { DataService } from '../../services/data-service/data.service';
import { ValidationService } from '../../services/validation/validation.service';
import { ValidatorBase } from '../../services/validation/validation';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Subject } from 'rxjs';
import { User } from '../../implementations/User/User';
import { IAuthResponse } from '../../interfaces/AuthResponce/IAuthResponce';
import { IErrorHandler } from '../../interfaces/ErrorHandler/IErrorHandler';
import { ErrorHandler } from '../../implementations/ErrorHandler/ErrorHandler';



@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [NgClass, FormsModule, CommonModule],
  providers: [DataService],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})

export class SignUpComponent extends ValidatorBase implements OnInit {

  constructor(
    private router: Router, 
    @Inject(DataService) private dataService: DataService,
    @Inject(ValidationService) private validService: ValidationService,
    @Inject(DataExchangeService) private dataExchange: DataExchangeService    
  )
  {
    super();
    
  }
  ngOnInit(): void {
    this.Init(7); 
    //Add pipe for User Data Transmitting      
  }

  @ViewChild('ButSubmit', { static: false }) SubButton! : ElementRef
  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();
  
errorHandler : IErrorHandler = new ErrorHandler();

  handleClose(value: boolean) {
    this.onChange.emit(value);
    this.sign_up_display = true;
  };
// fields
  nickname: string = '';
  password: string = '';
  name: string = '';
  surename: string = '';
  birthday: string = '';
  address: string = '';
  password2: string = '';
  
  all_fields : boolean = false;
  sign_up_display : boolean = true;
  error : string = ''

  registerUser() {
    if(!this.all_fields)
      return;

    let user: User = {
      nickname: this.nickname,
      password: this.password,      
      name: this.name,
      surename: this.surename,
      birthday: this.birthday,
      address: this.address
    };
           
    // action is here
    this.dataService.registerUser<IAuthResponse>(user).subscribe(
      (resp)  => {                  
          let u = 
          {
            nickname: resp!.nickname,
            surename : resp!.surename,
            name : resp!.name,
            address : resp!.address,
            birthday : resp!.birthday,
            password : ''
          };

          this.dataExchange.setUser(u);
          this.handleClean()        
          this.router.navigate(["/main"])                                      
      },
    err => 
    {
      this.dataExchange.errorTransfer = this.errorHandler.Handle(err, "Register", "/")
      this.router.navigate(["/reg-fail"]); 
    });                          
  };

  handleClean() {
    this.nickname = '';
    this.password = '';
    this.name = '',
    this.surename = '',
    this.birthday = '',
    this.address = ''
  }

  // Validation
  // Validate nickname
  nicknameChanged(value : string)
  {      
    this.validArray[0] = this.validService.ValidateTextNotEmpty(value);
    this.all_fields = super.CheckValidArray();
    super.enableElement(this.all_fields, this.SubButton);    
  }

  pass1Changed(arg0: string) {
    this.validArray[1] = this.validService.ValidateTextNotEmpty(arg0);
    this.all_fields = super.CheckValidArray();
    super.enableElement(this.all_fields, this.SubButton);
  }

  pass2Changed(arg0: string) {
    this.validArray[2] = this.validService.ValidateTextNotEmpty(arg0)
    && this.password2 == this.password;
    this.all_fields = super.CheckValidArray();
    super.enableElement(this.all_fields, this.SubButton);
  }

  nameChanged(value: string)
  {
    this.validArray[3] = this.validService.ValidateText(value);
    this.all_fields = super.CheckValidArray();
    super.enableElement(this.all_fields, this.SubButton);
  }

  surenameChanged(value : string)
  {
    this.validArray[4] = this.validService.ValidateText(value);
    this.all_fields = super.CheckValidArray();
    super.enableElement(this.all_fields, this.SubButton);
  }

  birthDayChanged(value : string)
  {
    this.validArray[5] = this.validService.ValidateDate(value);
    this.all_fields = super.CheckValidArray();
    super.enableElement(this.all_fields, this.SubButton);
  }

  addressChanged(value : string)
  {
    this.validArray[6] = this.validService.ValidateTextNotEmpty(value);
    this.all_fields = super.CheckValidArray();
    super.enableElement(this.all_fields, this.SubButton);
  }

  
}
