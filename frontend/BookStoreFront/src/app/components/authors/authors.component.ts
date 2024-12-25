import { Component, Inject } from '@angular/core';
import { AuthorFormComponent } from '../author-form/author-form.component';
import { DataService } from '../../services/data-service/data.service';
import { AutorEditFormComponent } from '../author-edit-form/autor-edit-form.component';
import { Author } from '../../implementations/Author/Author';
import { IErrorHandler } from '../../interfaces/ErrorHandler/IErrorHandler';
import { ErrorHandler } from '../../implementations/ErrorHandler/ErrorHandler';
import { Router } from '@angular/router';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { IAuthor } from '../../interfaces/Author/IAuthor';

@Component({
  selector: 'app-authors',
  standalone: true,
  imports: [AuthorFormComponent, AutorEditFormComponent],
  providers: [DataService],
  templateUrl: './authors.component.html',
  styleUrl: './authors.component.css'
})
export class AuthorsComponent {

  errorHandler : IErrorHandler = new ErrorHandler;

  public readonly DataExchange! : DataExchangeService;

  constructor(@Inject(DataService) private dataService: DataService,
  @Inject(Router) private router : Router,
  @Inject(DataExchangeService) private dataExchange : DataExchangeService) {
    this.DataExchange = dataExchange;
  };

  showAuthorForm = false;
  authorForm = false;
  editForm = false;

  showAuthorFormComponent() {
    this.authorForm = true;
    this.showAuthorForm = true;
  }

  hideAuthorFormComponent(value: boolean) {
    this.showAuthorForm = value;
  }

  showAuthorEditForm = false;
  authorEditForm = false;
  currentID = -1;

  editFormInitialized()
  {
    this.DataExchange.EditBook.next(this.currentID)
    this.editForm = true;
  }

  showAuthorEditFormComponent(id: number) {
    
    this.authorEditForm = true;
    this.showAuthorEditForm = true;
    this.currentID = id;

    if(this.editForm)
      {
        this.DataExchange.EditBook.next(this.currentID)
      }
  };

  hideAuthorEditFormComponent() {
    this.showAuthorEditForm = false;
  };
  
  getAuthors()
  {
    this.dataService.getAuthors().subscribe((resp) => 
      {
          if(resp["message"] == "Unauthorized")
          {
            this.dataExchange.errorTransfer = { error: resp["message"], action : 'Get Authors',
              route : "/"
             }
            this.router.navigate(["/reg-fail"]);
          }
          else
          {
            this.dataExchange.updateAllAuthors(resp as IAuthor[]);
          }
      }, 
      err => {
        this.dataExchange.errorTransfer = this.errorHandler.Handle(err, 'Get Authors', "/main");
        this.router.navigate(["/reg-fail"]);
      })  
  }

  ngOnInit() {
    this.getAuthors();
  }

  deleteAuthor(id: number) 
  {
    
    this.dataService.deleteAuthor(id).subscribe((resp) => 
      {
          if(resp["message"] == "Unauthorized")
          {
            this.dataExchange.errorTransfer = { error: resp["message"], action : 'Delete Author',
              route : "/"
             }
            this.router.navigate(["/reg-fail"]);
          }
          else
          {
            this.dataExchange.deleteAuthor(resp["id"]);
          }
      }, 
      err => {
        this.dataExchange.errorTransfer = this.errorHandler.Handle(err, 'Delte Author', "/main");
        this.router.navigate(["/reg-fail"]);
      })  
   
  };

}
