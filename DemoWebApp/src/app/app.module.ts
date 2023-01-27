import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HomeComponent } from './home/home.component';
import { SigninComponent } from './signin/signin.component';
import { RequestConfigService } from './services/requestConfig/request-config.service';
import { AuthenticationModule } from './Modules/authentication/authentication.module';
import { StoreComponent } from './store/store.component';
import { StoreModule } from './Modules/store/store.module';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    SigninComponent,
    StoreComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'signin', component: SigninComponent },
      { path: 'store', component: StoreComponent },
    ]),
  ],
  providers: [
    RequestConfigService,
    AuthenticationModule,
    StoreModule,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
