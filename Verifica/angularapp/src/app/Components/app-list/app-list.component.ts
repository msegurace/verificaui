import { Component, OnInit, ViewChild } from '@angular/core';
import { trigger, transition, query, style, animate } from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { MatDialog } from '@angular/material/dialog';
import { AlertService } from '../../Services/alert.service';
import { ApplicationService } from '../../Services/application.service';
import { AplicacionDto } from '../../Models/aplicacion.dto';

export interface DisplayColumn {
  def: string;
  label: string;
  hide: boolean;
}

@Component({
  selector: 'app-app-list',
  templateUrl: './app-list.component.html',
  styleUrls: ['./app-list.component.scss'],
  animations: [
    trigger('animation', [
      transition('* => *', [
        query(
          ':enter',
          [
            style({ transform: 'translateX(100%)', opacity: 0 }),
            animate('500ms', style({ transform: 'translateX(0)', opacity: 1 }))
          ],
          { optional: true }
        ),
        query(
          ':leave',
          [
            style({ transform: 'translateX(0)', opacity: 1 }),
            animate('500ms', style({ transform: 'translateX(100%)', opacity: 0 }))
          ],
          {
            optional: true
          }
        )
      ])
    ])
  ]
})
export class AppListComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  ELEMENT_DATA!: AplicacionDto[];
  dataSource = new MatTableDataSource<AplicacionDto>(this.ELEMENT_DATA);
  selection!: SelectionModel<AplicacionDto>;
  applications: string[] = [];
  selectedApp: string = 'all';
  add: string = 'Añadir';
  edit: string = 'Editar';
  delete: string = 'Borrar';
  value: string = '';
  isLoading: boolean = true;

  // Keep as main 'column mapper'
  displayedColumns: DisplayColumn[] = [
    { def: 'select', label: 'Select', hide: false },
    { def: 'id', label: 'Id', hide: false },
    { def: 'descripcion', label: 'Descripción', hide: false },
    { def: 'url', label: 'URL', hide: false },
    { def: 'origen', label: 'Origen', hide: false },
    { def: 'clasificacion_ens', label: 'Clasificación ENS', hide: false },
  ];

  // Used in the template
  disColumns!: string[];

  // Use for creating check box views dynamically in the template
  checkBoxList: DisplayColumn[] = [];

  constructor(
    public dialog: MatDialog,
    private service: ApplicationService,
    private alertService: AlertService) {
    
  }

  ngOnInit(): void {
    // Apply paginator
    this.dataSource.paginator = this.paginator;

    // Apply sort option
    this.dataSource.sort = this.sort;

    // Create instance of checkbox SelectionModel
    this.selection = new SelectionModel<AplicacionDto>(true, []);

    // Update with columns to be displayed
    this.disColumns = this.displayedColumns.map(cd => cd.def)

    // Get covid19 data from external rest api endpoint
    this.getAllApplications();
  }

  // This function filter data by input in the search box
  applyFilter(event: any): void {
    this.dataSource.filter = event.target.value.trim().toLowerCase();
  }

  // This function will be called when user click on select all check-box
  isAllSelected(): boolean {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle(): void {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach(row => this.selection.select(row));
  }

  // Add, Edit, Delete rows in data table
  openAddEditDialog(action: string, obj: any): void {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      data: obj,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result != null) {
        const action = result.data['action'];
        delete result.data['action'];
        if (action == this.add) {
          this.addRowData(result.data);
        } else if (action == this.edit) {
          this.updateRowData(result.data);
        } else {
          console.log(action);
        }
      }
    });
  }

  // Add a row into to data table
  addRowData(row_obj: any): void {
    const data = this.dataSource.data
    data.push(row_obj);
    this.dataSource.data = data;
  }

  // Update a row in data table
  updateRowData(row_obj: any): void {
    if (row_obj === null) { return; }
    const data = this.dataSource.data
    const index = data.findIndex((item) => item['id'] === row_obj.data['id']);
    if (index > -1) {
      data[index].id = row_obj.data['id'];
      data[index].descripcion = row_obj.data['descripcion'];
      data[index].url = row_obj.data['url'];
      data[index].origen = row_obj.data['origen'];
      data[index].clasificacion_ens = row_obj.data['clasificacion_ens'];
    }
    this.dataSource.data = data;
  }

  // Open confirmation dialog
  openDeleteDialog(len: number, obj: any): void {
    const options = {
      title: 'Delete?',
      message: `Está seguro de borrar ${len} filas?`,
      cancelText: 'NO',
      confirmText: 'SÍ'
    };

    // If user confirms, remove selected row from data table
    this.alertService.open(options);
    this.alertService.confirmed().subscribe(confirmed => {
      if (confirmed) {
        this.deleteRow(obj);
      }
    });
  }

  // Delete a row by 'row' delete button
  deleteRow(row_obj: any): void {
    const data = this.dataSource.data
    const index = data.findIndex((item) => item['id'] === row_obj['id']);
    if (index > -1) {
      data.splice(index, 1);
    }
    this.dataSource.data = data;
  }

  // Fill data table
  public getAllApplications(): void {
    let resp = this.service.getall();
    resp.then((report) => {
      this.isLoading = false;
      this.dataSource.data = report.items as AplicacionDto[];
    })
  }

  // Fill on selected option
  public async onSelectApp(): Promise<void> {
    this.selection.clear();
    if (this.selectedApp === 'all') {
      this.getAllApplications();
    } else {
      console.log(this.selectedApp);
      await this.service.get(this.selectedApp.toString()).then((report) => {
        console.log(report.descripcion);
        this.dataSource.data.map(app => app.id === report.id)
      });
    }
  }

  // Show/Hide check boxes 
  showCheckBoxes(): void {
    this.checkBoxList = this.displayedColumns;
  }

  hideCheckBoxes(): void {
    this.checkBoxList = [];
  }

  toggleForm(): void {
    this.checkBoxList.length ? this.hideCheckBoxes() : this.showCheckBoxes();
  }

  // Show/Hide columns
  hideColumn(event: any, item: string) {
    this.displayedColumns.forEach(element => {
      if (element['def'] == item) {
        element['hide'] = event.checked;
      }
    });
    this.disColumns = this.displayedColumns.filter(cd => !cd.hide).map(cd => cd.def)
  }
}
