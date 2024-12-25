import { Component, Inject, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Route, Router, RouterLink } from '@angular/router';
import { BooksComponent } from '../books/books.component';
import { AuthorsComponent } from '../authors/authors.component';
import { DataService } from '../../services/data-service/data.service';
import { ErrorHandler } from '../../implementations/ErrorHandler/ErrorHandler';
import { IErrorHandler } from '../../interfaces/ErrorHandler/IErrorHandler';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [RouterLink, BooksComponent, AuthorsComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css'
})
export class MainPageComponent {

  constructor(
    @Inject(DataService) private dataService : DataService,
    @Inject(Router) private router : Router,
    @Inject(DataExchangeService) private dataExchangeService : DataExchangeService   
  )
  {}

  errorHandler : IErrorHandler = new ErrorHandler;
        
  onLogOutClicked()
  {
    this.dataService.logoutUser().subscribe(
      resp =>
      {       
          this.router.navigate(["/"]);   
          this.dataExchangeService.deleteUser();                
      },
      err => {                           
        this.dataExchangeService.errorTransfer = this.errorHandler.Handle(err, "Logout", "/")
        this.router.navigate(["/reg-fail"]);    
      } 
    );    
  }

}
