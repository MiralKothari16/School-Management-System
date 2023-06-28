import { Component, OnInit } from '@angular/core';
import { Iadmin } from 'src/app/Interface/iadmin';
import { AdminService } from 'src/app/Service/admin.service';

@Component({
  selector: 'app-adminmanagement',
  templateUrl: './adminmanagement.component.html',
  styleUrls: ['./adminmanagement.component.scss']
})
export class AdminmanagementComponent implements OnInit {
  public admin !: Iadmin[];
  public updatedId!: string | null;
  public isLoading: boolean = true;
  public isError: boolean = false;

  constructor(private adminservice: AdminService) { }

  ngOnInit(): void {
    this.getadmins();
  }
  getadmins() {
    this.isLoading = true;
    return this.adminservice.getAdmins().subscribe({
      next: (res) => {
        this.admin = res.data;
        this.isLoading = false;
        this.isError = false;
      },
      error: (error) => {
        console.log(error);
        this.isLoading = false;
        this.isError = true;
      }
    });
  }
  OpenAdminForm() { }
  editadmin(admin: Iadmin) {

  }
}
