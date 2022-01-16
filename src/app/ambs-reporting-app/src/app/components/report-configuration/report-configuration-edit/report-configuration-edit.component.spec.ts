import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportConfigurationEditComponent } from './report-configuration-edit.component';

describe('ReportConfigurationEditComponent', () => {
  let component: ReportConfigurationEditComponent;
  let fixture: ComponentFixture<ReportConfigurationEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportConfigurationEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportConfigurationEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
