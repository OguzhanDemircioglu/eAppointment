import {DepartmentModel} from "./department.model";

export class DoctorModel {
  id: string = "";
  fullName: string = "";
  firstName: string = "";
  lastName: string = "";
  department: DepartmentModel = new DepartmentModel();
  departmentValue: number = 0;
}
