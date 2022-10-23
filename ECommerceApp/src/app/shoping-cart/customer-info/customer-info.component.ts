import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customer-info',
  templateUrl: './customer-info.component.html',
  styleUrls: ['./customer-info.component.css']
})
export class CustomerInfoComponent implements OnInit {

  myform = new FormGroup({
    custName: new FormControl('', [Validators.required, Validators.minLength(6),Validators.maxLength(25), Validators.pattern('^[^\\s].*[^\\s]$')]),
    custEmail: new FormControl('', [Validators.required, Validators.email]),
    custPhone: new FormControl('', [Validators.required, Validators.minLength(6),Validators.maxLength(25), Validators.pattern('^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$')]),
  });
  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  OnSubmit(){
    if (this.myform.invalid) return;
    if(this.myform.valid) this.router.navigate(['/ConfirmationPage']);
  }

}
