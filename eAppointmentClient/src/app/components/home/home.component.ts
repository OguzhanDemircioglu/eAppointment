import {Component, ElementRef, ViewChild} from '@angular/core';
import {RouterLink} from "@angular/router";
import {FormsModule, NgForm} from "@angular/forms";
import {departments, formatDate} from "../../constats";
import {DoctorModel} from "../../models/doctor.model";
import {CommonModule} from "@angular/common";
import {DxSchedulerModule} from 'devextreme-angular';
import {PatientModel} from "../../models/patient.model";
import {HttpService} from "../../services/http.service";
import {SwalService} from "../../services/swal.service";
import {AppointmentModel} from "../../models/appointment.model";
import {CreateAppointmentModel} from "../../models/create-appointment.model";
import {FormValidateDirective} from "form-validate-angular";

declare const $: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    FormsModule,
    DxSchedulerModule,
    FormValidateDirective
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  departments = departments;
  doctors: DoctorModel[] = [];
  appointments: AppointmentModel[] = [];
  selectedDepartmentId: number = 0;
  selectedDoctorId: string = "";
  createModel: CreateAppointmentModel = new CreateAppointmentModel();

  @ViewChild("addModalCloseBtn") addModalCloseButton: ElementRef<HTMLButtonElement> | undefined;

  constructor(
    private http: HttpService,
    private swal: SwalService
  ) {
  }

  GetAllDoctorsByDepartmentId() {
    if (this.selectedDepartmentId > 0) {
      this.http.post<DoctorModel[]>("Doctor/GetAllDoctorsByDepartmentId",
        {departmentValue: this.selectedDepartmentId}, (res) => {
          this.doctors = res.data;
        })
    }
  }

  GetPatientByPatientIdentityNumber() {
    this.http.post<PatientModel>("Patient/GetPatientByPatientIdentityNumber", {identityNumber: this.createModel.identityNumber}, (res) => {
      if (res.data) {
        this.createModel.identityNumber = res.data.identityNumber;
        this.createModel.city = res.data.city;
        this.createModel.town = res.data.town;
        this.createModel.firstName = res.data.firstName;
        this.createModel.lastName = res.data.lastName;
        this.createModel.fullAddress = res.data.fullAddress;
        this.createModel.patientId = res.data.id;
        this.createModel.doctorId = this.selectedDoctorId;
      }
    })
  }

  GetAllAvailableAppointments() {
    if (this.selectedDoctorId != "") {
      this.http.post<AppointmentModel[]>("Appointment/GetAllAvailableAppointments", {doctorId: this.selectedDoctorId}, (res) => {
        this.appointments = res.data;
      })
    }
  }

  create(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("Appointment/Create", this.createModel, (res) => {
        if (res) {
          this.swal.callToast(res.data);
          this.addModalCloseButton?.nativeElement.click();
          this.createModel = new CreateAppointmentModel();
          this.GetAllAvailableAppointments();
        }
      });
    }
  }

  onAppointmentFormOpening(e: any) {
    e.cancel = true;
    this.createModel.startDate = formatDate(new Date(e.appointmentData.startDate), "MM.dd.yyyy HH:mm");
    this.createModel.endDate = formatDate(new Date(e.appointmentData.endDate), "MM.dd.yyyy HH:mm");
    this.createModel.doctorId = this.selectedDoctorId;

    $("#addModal").modal("show");
  }

  onAppointmentDeleted(e: any) {
    e.cancel = true;
  }

  onAppointmentDeleting(e: any) {
    e.cancel = true;
    this.swal.callSwalForDelete(() => {
      this.http.post<string>("Appointment/Delete", {id: e.appointmentData.id}, (res) => {
        this.swal.callToast(res.data);
        this.GetAllAvailableAppointments();
      })
    });
  }

  onAppointmentUpdating(e: any) {
    e.cancel = true;
    this.http.post<string>("Appointment/Update",
      {
        id: e.oldData.id,
        startDate: e.newData.startDate.replace("T", " "),
        endDate: e.newData.endDate.replace("T", " ")
      },
      (res) => {
        this.swal.callToast(res.data);
        this.GetAllAvailableAppointments();
      });
  }
}
