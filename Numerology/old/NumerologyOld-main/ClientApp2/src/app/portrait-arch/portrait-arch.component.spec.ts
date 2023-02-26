import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PortraitArchComponent } from './portrait-arch.component';

describe('PortraitArchComponent', () => {
  let component: PortraitArchComponent;
  let fixture: ComponentFixture<PortraitArchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PortraitArchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PortraitArchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
