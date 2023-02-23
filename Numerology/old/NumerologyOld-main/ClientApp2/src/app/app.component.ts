import { ToastrService } from 'ngx-toastr';
import {MediaMatcher} from '@angular/cdk/layout';
import {ChangeDetectorRef, Component, OnDestroy} from '@angular/core';
import { SidebarService } from '../services/sidenav.service';
import { NameEndpoint } from 'src/endpoints/name.endpoint';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ClientApp2';

  Nwm: any[];

  constructor(private nameEndpoint: NameEndpoint, private toastr: ToastrService, private changeDetectorRef: ChangeDetectorRef, private media: MediaMatcher, private sidebarservice: SidebarService)
  {
  }

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.toastr.success('Udanej pracy :)', 'Hi!', {
      positionClass: 'toast-bottom-right',
      progressBar: true,
      
    });
  }

  onlyTesting()
  {
    var request: any = {
      page: 0,
      countOnPage: 10
    };
    this.nameEndpoint.getNamesEndpoint(request).subscribe(result => this.onLoadedNames(result),
                                                          error => this.onLoadNamesError(error));
  }

  onLoadedNames(data: any)
  {
    this.toastr.success('Udalo sie pobrac dane', 'Success!', {
      positionClass: 'toast-bottom-right'
    });
    this.Nwm = data;
    //debugger;
  }

  onLoadNamesError(err: any)
  {
    this.toastr.error('Blad', 'Error!', {
      positionClass: 'toast-bottom-right'
    });
  }


  toggleSidebar() {
    this.sidebarservice.setSidebarState(!this.sidebarservice.getSidebarState());
  }
  toggleBackgroundImage() {
    this.sidebarservice.hasBackgroundImage = !this.sidebarservice.hasBackgroundImage;
  }
  getSideBarState() {
    return this.sidebarservice.getSidebarState();
  }

  hideSidebar() {
    this.sidebarservice.setSidebarState(true);
  }
}
