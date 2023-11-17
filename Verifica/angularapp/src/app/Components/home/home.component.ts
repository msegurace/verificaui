import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderMenus } from 'src/app/Models/header-menu.dto';
import { HeaderMenusService } from 'src/app/Services/header-menus.service';
import { AplicacionDto } from '../../Models/aplicacion.dto';
import { ApplicationService } from '../../Services/application.service';
import { SharedService } from '../../Services/shared.service';
import { Observable } from 'rxjs';
import { GenericResponse } from '../../Models/GenericResponse';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  response?: GenericResponse;
  apps?: AplicacionDto[] ;
  showButtons: boolean = false;

  constructor(
    private appService: ApplicationService,
    private sharedService: SharedService,
    private router: Router,
    private headerMenusService: HeaderMenusService
  ) {
    this.loadApps();
  }

  ngOnInit(): void {
    this.headerMenusService.headerManagement.subscribe(
      (headerInfo: HeaderMenus) => {
        if (headerInfo) {
          this.showButtons = headerInfo.showAuthSection;
        }
      }
    );
  }
  private async loadApps(): Promise<void> {
    const userId = sessionStorage.getItem('username');
    if (userId) {
      this.showButtons = true;
    }
    try {
      await this.appService.getall().then(
        resp => {
          this.response = resp;
          this.apps = resp.items;
        }
      );
      
    } catch (error: any) {
      this.handleError(error.error);
    }
  }

  private handleError = (errorResponse: any): void => {
    this.sharedService.errorLog(errorResponse);
  };
 
}
