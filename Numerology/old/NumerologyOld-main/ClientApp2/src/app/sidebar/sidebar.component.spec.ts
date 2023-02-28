import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiebarComponent } from './diebar.component';

describe('DiebarComponent', () => {
  let component: DiebarComponent;
  let fixture: ComponentFixture<DiebarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DiebarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
