import {ActivatedRouteSnapshot, CanDeactivate, Router, RouterStateSnapshot} from '@angular/router';
import {LearnComponent} from '../../learn/learn.component';
import {Injectable} from '@angular/core';
import {AlertifyService} from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class LearnCanDeactivateGuard implements CanDeactivate<LearnComponent> {

  constructor(private alertifyService: AlertifyService,
              private router: Router) { }

  canDeactivate(
    component: LearnComponent,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState?: RouterStateSnapshot): boolean {
    if (component.learnInProgress) {
      return window.confirm('Opuszczenie spowoduje utratę postępu. Czy kontynuować?');
    } else {
      return true;
    }
  }
}
