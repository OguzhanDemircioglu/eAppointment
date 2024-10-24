import { CommonModule } from '@angular/common';
import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { FormValidateDirective } from 'form-validate-angular';
import { UserPipe } from '../../pipes/user.pipe';
import { UserModel } from '../../models/user.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { RoleModel } from '../../models/role.model';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, FormValidateDirective, UserPipe],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit{
  users: UserModel[] = [];
  roles: RoleModel[] = [];

  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel: UserModel = new UserModel();
  updateModel: UserModel = new UserModel();

  search: string = "";

  constructor(
    private http: HttpService,
    private swal: SwalService
  ){}

  ngOnInit(): void {
    this.getAll();
    this.getAllRoles();
  }

  getAll(){
    this.http.post<UserModel[]>("User/GetAll", {}, (res)=> {
      this.users = res.data;
    });
  }

  getAllRoles(){
    this.http.post<RoleModel[]>("Role/GetAllRoles",{}, res=> {
      this.roles = res.data;
    });
  }

  add(form: NgForm){
    if(form.valid){
      this.http.post<string>("User/Create",this.createModel,(res)=> {
        this.swal.callToast(res.data,"success");
        this.getAll();
        this.addModalCloseBtn?.nativeElement.click();
        this.createModel = new UserModel();
      });
    }
  }

  delete(id: string, fullName: string){
    this.swal.callSwalForDelete(() => {
      this.http.post<string>("User/DeleteById", {id: id}, (res)=> {
        this.swal.callToast(res.data,"info");
        this.getAll();
      })
    })
  }

  get(data: UserModel){
    this.updateModel = {...data};
    console.log(this.updateModel);

  }

  update(form:NgForm){
    if(form.valid){
      this.http.post<string>("User/Update",this.updateModel,(res)=> {
        this.swal.callToast(res.data,"success");
        this.getAll();
        this.updateModalCloseBtn?.nativeElement.click();
      });
    }
  }
}
