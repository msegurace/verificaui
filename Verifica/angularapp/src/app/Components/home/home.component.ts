import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderMenus } from 'src/app/Models/header-menu.dto';
import { HeaderMenusService } from 'src/app/Services/header-menus.service';
import { AplicacionDto } from '../../Models/aplicacion.dto';
import { ApplicationService } from '../../Services/application.service';
import { SharedService } from '../../Services/shared.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  apps!: AplicacionDto[];
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
      this.apps = await this.appService.getall();
    } catch (error: any) {
      this.handleError(error.error);
    }
  }

  private handleError = (errorResponse: any): void => {
    this.sharedService.errorLog(errorResponse);
  };
 
}
