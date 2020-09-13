import {Injectable} from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  showConfirmAlert(message: string, okCallBack: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okCallBack();
      }
      else { }
    });
  }

  showSuccessAlert(message: string) {
    alertify.success(message);
  }

  showErrorAlert(message: string) {
    alertify.error(message);
  }

  showWarningAlert(message: string) {
    alertify.warning(message);
  }

  showMessageAlert(message: string) {
    alertify.message(message);
  }
}
