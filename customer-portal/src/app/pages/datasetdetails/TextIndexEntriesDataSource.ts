import {DataSource, CollectionViewer} from "@angular/cdk/collections";
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Text_Index_Entry } from '../datatypes/entries/TextIndexEntry';
import { DatasetEntriesService } from '../services/datasetentries.service';
import { CompoundIndexEntry } from "../datatypes/entries/CompoundTextIndexEntry";

export class TextIndexEntriesDataSource implements DataSource<CompoundIndexEntry>{
    private entriesSubject = new BehaviorSubject<CompoundIndexEntry[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private id: string, private entriesService : DatasetEntriesService){}

    connect(collectionViewer: CollectionViewer) : Observable<CompoundIndexEntry[]>{
        return this.entriesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer) : void{
        this.entriesSubject.complete();
        this.loadingSubject.complete();
    }

    loadDatasets(start: number, count: number){
        this.loadingSubject.next(true);
        this.entriesService.get(this.id, start, count).pipe(
            catchError(() => of([])),
            finalize(() => this.loadingSubject.next(false))
        )
        .subscribe(entries => 
            {
                this.entriesSubject.next(entries); 
            }
            );
    }
}