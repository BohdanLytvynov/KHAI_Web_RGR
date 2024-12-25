import { NgClass } from '@angular/common';
import { Component, ElementRef, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ValidatorBase } from '../../services/validation/validation';
import { IErrorHandler } from '../../interfaces/ErrorHandler/IErrorHandler';
import { ErrorHandler } from '../../implementations/ErrorHandler/ErrorHandler';
import { ValidationService } from '../../services/validation/validation.service';
import { DataService } from '../../services/data-service/data.service';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { Author } from '../../implementations/Author/Author';
import { Router } from '@angular/router';
import { IAuthor } from '../../interfaces/Author/IAuthor';

@Component({
  selector: 'app-author-form',
  standalone: true,
  imports: [NgClass, FormsModule],
  templateUrl: './author-form.component.html',
  styleUrl: './author-form.component.css'
})
export class AuthorFormComponent extends ValidatorBase implements OnInit {
  ngOnInit(): void {
    this.Init(3);    
  }

  constructor(
    @Inject(ValidationService) private validation : ValidationService,
    @Inject(DataService) private dataService : DataService,
    @Inject(DataExchangeService) private dataExchange : DataExchangeService,
    @Inject(Router) private router : Router
    )
  {
    super();
  }

  errorHandler : IErrorHandler = new ErrorHandler();

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();
  @ViewChild('SubmitButton', {static: false}) Submit! : ElementRef


  handleClose(value: boolean) {
    this.onChange.emit(value)
  };

  authorID: number = -1;
  authorName: string = '';
  authorSureName: string = '';
  authorBirthday: string = '';

  all_fields_correct : boolean = false;

  addAuthor() {

    if(!this.all_fields_correct)
      return;
    
    let author : Author = { id: 0, 
      name : this.authorName, 
      surename : this.authorSureName,
      birthDate : this.authorBirthday
     }

    this.dataService.addAuthor(author)
    .subscribe(resp => 
    {
      if(resp["message"] == "Unauthorized")
        {
          this.dataExchange.errorTransfer = { error: resp["message"], action : 'Add Author',
            route : "/"
           }
          this.router.navigate(["/reg-fail"]);
        }
        else
        {
           this.dataExchange.addAuthor(resp as IAuthor)
        }

        this.handleClean();
        this.handleClose(false)
    },
    err=>
    {
      this.dataExchange.errorTransfer = this.errorHandler.Handle(err, 'Add Author', "/main");
      this.router.navigate(["/reg-fail"]);
    }
    )

    this.handleClean()
    this.handleClose(false)
  };

  handleClean() {
    this.authorID = 0;
    this.authorName = '';
    this.authorSureName = '';
    this.authorBirthday = '';
  };

  //Validation
  onNameChanged(value : string)
  {
    this.validArray[0] = this.validation.ValidateText(value);
    this.all_fields_correct = this.CheckValidArray();
    this.enableElement(this.all_fields_correct, this.Submit)
  }

  onSureNamwChanged(value : string)
  {
    this.validArray[1] = this.validation.ValidateText(value);
    this.all_fields_correct = this.CheckValidArray();
    this.enableElement(this.all_fields_correct, this.Submit)
  }

  onBirthdayChanged(value : string)
  {
    this.validArray[2] = this.validation.ValidateDate(value);
    this.all_fields_correct = this.CheckValidArray();
    this.enableElement(this.all_fields_correct, this.Submit)
  }

}
