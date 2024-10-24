import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {RouterLink} from "@angular/router";
import {DoctorModel} from "../../models/doctor.model";
import {HttpService} from "../../services/http.service";
import {CommonModule} from "@angular/common";
import {FormsModule, NgForm} from "@angular/forms";
import {departments} from "../../constats";
import {FormValidateDirective} from "form-validate-angular";
import {SwalService} from "../../services/swal.service";
import {DoctorPipe} from "../../pipes/doctor.pipe";

@Component({
  selector: 'app-doctors',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, FormValidateDirective, DoctorPipe],
  templateUrl: './doctors.component.html',
  styleUrl: './doctors.component.scss'
})
export class DoctorsComponent implements OnInit {
  doctors: DoctorModel[] = [];
  createModel: DoctorModel = new DoctorModel();
  updateModel: DoctorModel = new DoctorModel();
  departments = departments;
  search: string = "";

  @ViewChild("addModalCloseBtn") addModalCloseButton: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModelCloseBtn") updateModelCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  ngOnInit(): void {
    this.getAll();
  }

  constructor(
    private http: HttpService,
    private swal: SwalService
  ) {
  }

  getAll() {
    this.http.post<DoctorModel[]>("Doctor/GetAll", {}, (res) => {
      this.doctors = res.data;
    })
  }

  add(form: NgForm) {
    if (this.createModel.departmentValue === 0) {
      alert("Select Department!");
      return;
    }
    if (form.valid) {
      this.http.post<string>("Doctor/Create", this.createModel, (res) => {
        if (res) {
          this.swal.callToast(res.data);
          this.getAll();
          this.addModalCloseButton?.nativeElement.click();
          this.createModel = new DoctorModel();
        }
      });
    }
  }

  delete(id: string) {
    this.swal.callSwalForDelete(() => {
      this.http.post<string>("Doctor/Delete", {id: id}, (res) => {
        this.swal.callToast(res.data);
        this.getAll();
      })
    });
  }

  get(data: DoctorModel) {
    this.updateModel = {...data};
    this.updateModel.departmentValue = data.department.value;
  }

  update(form: NgForm) {
    if (this.updateModel.departmentValue === 0) {
      this.swal.callToast("Select Department!", "error");
      return;
    }
    if (form.valid) {
      this.http.post<string>("Doctor/Update", this.updateModel, (res) => {
        if (res) {
          this.swal.callToast(res.data);
          this.getAll();
          this.updateModelCloseBtn?.nativeElement.click();
          this.updateModel = new DoctorModel();
        }
      });
    }
  }
}
