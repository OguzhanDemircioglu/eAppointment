import {Injectable} from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SwalService {

  constructor() {
  }

  callToast(title: string, icon: SweetAlertIcon = "success") {
    Swal.fire({
      title: title,
      timer: 3000,
      icon: icon,
      showCancelButton: false,
      showCloseButton: false,
      toast: true,
      position: "top-right"
    })
  }

  callSwalForDelete(callback:()=>void) {
    Swal.fire({
      icon: "question",
      text: "Silmek İstiyor Musunuz",
      showCancelButton: true,
      showConfirmButton: true,
      confirmButtonText:"Delete",
      position: "center"
    }).then(res=>{
      if(res.isConfirmed){
        callback();
      }
    })
  }
}

export type SweetAlertIcon = 'success' | 'error' | 'warning' | 'info' | 'question'
