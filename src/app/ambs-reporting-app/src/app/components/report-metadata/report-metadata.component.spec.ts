import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportMetadataComponent } from './report-metadata.component';

describe('ReportMetadataComponent', () => {
  let component: ReportMetadataComponent;
  let fixture: ComponentFixture<ReportMetadataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportMetadataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportMetadataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
