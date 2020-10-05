import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { InboxViewComponent } from './inbox-view/inbox-view.component';
import { UserInputComponent } from './user-input/user-input.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ChatComponent } from './chat/chat.component';

export function getToken() {
  return localStorage.getItem('fancy-chat-jwt');
}

@NgModule({
  declarations: [
    AppComponent,
    InboxViewComponent,
    UserInputComponent,
    LoginComponent,
    RegisterComponent,
    ChatComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: getToken
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
