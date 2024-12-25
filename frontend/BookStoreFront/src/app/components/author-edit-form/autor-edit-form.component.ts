import { NgClass } from '@angular/common';
import { Component, ElementRef, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../services/data-service/data.service';
import { ValidationService } from '../../services/validation/validation.service';
import { ValidatorBase } from '../../services/validation/validation';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { Author } from '../../implementations/Author/Author';
import { Router } from '@angular/router';
import { IErrorHandler } from '../../interfaces/ErrorHandler/IErrorHandler';
import { ErrorHandler } from '../../implementations/ErrorHandler/ErrorHandler';

@Component({
  selector: 'app-autor-edit-form',
  standalone: true,
  imports: [NgClass, FormsModule],
  providers: [DataService],
  templateUrl: './autor-edit-form.component.html',
  styleUrl: './autor-edit-form.component.css'
})
export class AutorEditFormComponent extends ValidatorBase implements OnInit {

  constructor(@Inject(DataService) private dataService: DataService,
@Inject(ValidationService) private validation : ValidationService,
@Inject(DataExchangeService) private dataExchange : DataExchangeService,
@Inject(Router) private router : Router)   
 {
    super();
 }

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();
  @ViewChild('Submit', { static: false }) Submit! : ElementRef
  @Output() onInit = new EventEmitter();
  @Input() id : number = 0;

  handleClose(value: boolean) {
    this.onChange.emit(value)
  };

  author: Author  = {
    id: -1,
    name: '',
    surename: '',
    birthDate: ''
  };

  all_fields_correct : boolean = true;

  errorHandler : IErrorHandler = new ErrorHandler();

  ngOnInit() { 
    this.Init(3)
    this.author = this.dataExchange.getAuthorById(this.id)!; 
    this.validArray.fill(true);    
       
    this.dataExchange.EditBook.subscribe(
      (id) => { this.author = this.dataExchange.getAuthorById(id)!         
      })

      this.onInit.emit();
  }
  
  editAuthor() {
    if(!this.all_fields_correct)
      return;
   
    this.dataService.editAuthor(this.author)
    .subscribe( resp =>
    {
      if(resp["message"] == "Unauthorized")
        {
          this.dataExchange.errorTransfer = { error: resp["message"], 
            action : 'Edit book', route:"/" }
          this.router.navigate(["/reg-fail"]);
        }
        else
        {            
          this.dataExchange.editAuthor(resp as Author);
        }
      },
        err => {
          this.dataExchange.errorTransfer = this.errorHandler
          .Handle(err, `Edit Author`, "/main");
          this.router.navigate(["/reg-fail"]);
    });

    this.handleClose(false)
  };

  onAuthorNameChanged(value : string)
  {
      this.validArray[0] = this.validation.ValidateText(value);
      this.all_fields_correct = this.CheckValidArray()
      this.enableElement(this.all_fields_correct, this.Submit);
  }

  onAuthorSurenameChanged(value : string)
  {
    this.validArray[1] = this.validation.ValidateText(value);
      this.all_fields_correct = this.CheckValidArray()
      this.enableElement(this.all_fields_correct, this.Submit);
  }

  onAuthorBirthdayChanged(value : string)
  {
    this.validArray[2] = this.validation.ValidateDate(value);
      this.all_fields_correct = this.CheckValidArray()
      this.enableElement(this.all_fields_correct, this.Submit);
  }

}
