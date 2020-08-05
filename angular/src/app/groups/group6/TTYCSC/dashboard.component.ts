import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { AppIncomeStatisticsDateInterval } from '@shared/AppEnums';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DashboardYCSCServiceProxy, YCSCStatisticsOutput_DTO } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';

@Component({
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.less'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class GR6_Dashboard extends AppComponentBase implements AfterViewInit, OnInit {

    constructor(
        injector: Injector,
        private _dateTimeService: DateTimeService,
        private dashboardYCSCService: DashboardYCSCServiceProxy
    ) {
        super(injector);
    }

    init(): void {
        this.selectedIncomeStatisticsDateInterval = AppIncomeStatisticsDateInterval.Daily;
    }

    ngOnInit(): void {
        this.init();
    }

    @ViewChild('DashboardDateRangePicker') dateRangePickerElement: ElementRef;
    @ViewChild('YCSCStatisticsChart') ycscStatisticsChart: ElementRef;

    loading = false;
    loadingIncomeStatistics = false;
    isInitialized: boolean;
    initialStartDate: moment.Moment = moment().add(-7, 'days').startOf('day');
    initialEndDate: moment.Moment = moment().endOf('day');
    ycscStatisticsData: YCSCStatisticsOutput_DTO[];
    appIncomeStatisticsDateInterval = AppIncomeStatisticsDateInterval;
    selectedIncomeStatisticsDateInterval: number;
    ycscStatisticsHasData: boolean;
    total_request: any;
    total_no: any;
    total_was: any;
    total_ing: any;

    percent_no: any;
    percent_was: any;
    percent_ing: any;


    selectedDateRange = {
        startDate: this.initialStartDate,
        endDate: this.initialEndDate
    };

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.createDateRangePicker();
            this.getDashboardStatisticsData();
            this.bindToolTipForIncomeStatisticsChart($(this.ycscStatisticsChart.nativeElement));
            mApp.initScroller($('.m-scrollable'), {});
        }, 0);
    }

    createDateRangePicker(): void {
        $(this.dateRangePickerElement.nativeElement)
            .daterangepicker(
                $.extend(true, this._dateTimeService.createDateRangePickerOptions(), this.selectedDateRange),
                (start: moment.Moment, end: moment.Moment, label: any) => {
                    this.selectedDateRange.startDate = start;
                    this.selectedDateRange.endDate = end;
                    this.getDashboardStatisticsData();
                });
    }

    getDashboardStatisticsData(): void {
        this.loading = true;
        this.dashboardYCSCService
            .getYCSCStatistics(
                this.selectedIncomeStatisticsDateInterval,
                this.selectedDateRange.startDate,
                this.selectedDateRange.endDate
            )
            .subscribe(result => {
                this.ycscStatisticsData = result;
                this.drawIncomeStatisticsChart(result);
                this.loading = false;
            });
    }


    /*
     * Income statistics line chart
     */


    normalizeIncomeStatisticsData(data: string | any[]): Array<any> {
        const chartData = [];
        for (let i = 0; i < data.length; i++) {
            const point = new Array(2);
            point[0] = moment(data[i].date).utc().valueOf();
            point[1] = data[i].amount;
            chartData.push(point);
        }

        return chartData;
    }

    drawIncomeStatisticsChart(data: any[]): void {
        this.ycscStatisticsHasData = (data && data.length > 0);
        if (!this.ycscStatisticsHasData) {
            return;
        }

        const self = this;
        let normalizedData = [];
        let _info = [];
        data.map((a: YCSCStatisticsOutput_DTO, index) => {
            if (a.status_YCSC === "TTYCSC0000001") { this.total_no = a.total_Status; _info[index] = { color1: '#f4516c', color2: '#f99fae', label: this.l('GR6_DC') } }
            else if (a.status_YCSC === "TTYCSC0000002") { this.total_ing = a.total_Status; _info[index] = { color1: '#716aca', color2: '#b7b4e4', label: this.l('GR6_DTH') } }
            else { this.total_was = a.total_Status; _info[index] = { color1: '#34bfa3', color2: '#9be4d5', label: this.l('GR6_DHT') } }
            normalizedData.push(this.normalizeIncomeStatisticsData(a.ycscStatistics));
        });

        this.total_request = this.total_ing + this.total_no + this.total_was;

        this.percent_no = Math.round((this.total_no / this.total_request) * 100);
        this.percent_ing = Math.round((this.total_ing / this.total_request) * 100);
        this.percent_was = Math.round((this.total_was / this.total_request) * 100);

        let chartData = [];
        normalizedData.map((a, index) => {
            chartData.push(
                {
                    data: a,
                    lines: {
                        fill: 0.2,
                        lineWidth: 1
                    },
                    color: [_info[index].color2]
                }, {
                data: a,
                points: {
                    show: true,
                    fill: true,
                    radius: 4,
                    fillColor: _info[index].color1,
                    lineWidth: 2
                },
                color: _info[index].color1,
                shadowSize: 1
            }, {
                label: _info[index].label,
                data: a,
                lines: {
                    show: true,
                    fill: false,
                    lineWidth: 3
                },
                color: _info[index].color1,
                shadowSize: 0
            });
        });
        setTimeout(() => {
            ($ as any).plot($(self.ycscStatisticsChart.nativeElement),

                chartData
                ,
                {
                    xaxes: [{
                        axisLabel: "DMMMMMMMMMMMMMMMMMMMM",
                        axisLabelUseCanvas: true,
                        axisLabelFontSizePixels: 12,
                        axisLabelFontFamily: 'Verdana, Arial',
                        axisLabelPadding: 10,
                        rotateTicks: 45,
                        mode: 'time',
                        timeformat: "%d/%m/%Y",
                        minTickSize: [1, 'day'],
                        font: {
                            lineHeight: 20,
                            style: 'normal',
                            variant: 'small-caps',
                            color: '#6F7B8A',
                            size: 10
                        }
                    }],
                    yaxis: {
                        axisLabel: "kkk",
                        ticks: 5,
                        tickDecimals: 0,
                        tickColor: '#eee',
                        font: {
                            lineHeight: 14,
                            style: 'normal',
                            variant: 'small-caps',
                            color: '#6F7B8A'
                        }
                    },
                    grid: {
                        hoverable: true,
                        clickable: false,
                        tickColor: '#eee',
                        borderColor: '#eee',
                        borderWidth: 1,
                        margin: {
                            bottom: 20
                        }
                    }
                });
        }, 0);
    }

    incomeStatisticsDateIntervalChange(interval: number) {
        this.selectedIncomeStatisticsDateInterval = interval;
        this.refreshIncomeStatisticsData();
    }

    refreshIncomeStatisticsData(): void {
        // this.loadingIncomeStatistics = true;
        // this.dashboardYCSCService.getYCSCStatistics(
        //     this.selectedIncomeStatisticsDateInterval,
        //     this.selectedDateRange.startDate,
        //     this.selectedDateRange.endDate)
        //     .subscribe((result: { incomeStatistics: any; }) => {
        //         this.drawIncomeStatisticsChart(result.incomeStatistics);
        //         this.loadingIncomeStatistics = false;
        //     });
        this.getDashboardStatisticsData();
    }

    bindToolTipForIncomeStatisticsChart(incomeStatisticsChartContainer: any): void {
        let incomeStatisticsChartLastTooltipIndex = null;

        const removeChartTooltipIfExists = () => {
            const $chartTooltip = $('#chartTooltip');
            if ($chartTooltip.length === 0) {
                return;
            }

            $chartTooltip.remove();
        };

        const showChartTooltip = (x: number, y: number, Label: string, value: string) => {
            removeChartTooltipIfExists();
            $('<div id=\'chartTooltip\' class=\'chart-tooltip\'>' + Label + '<br/>' + value + '</div >')
                .css({
                    position: 'absolute',
                    display: 'none',
                    top: y - 60,
                    left: x - 40,
                    border: '0',
                    padding: '2px 6px',
                    opacity: '0.9'
                })
                .appendTo('body')
                .fadeIn(200);
        };

        incomeStatisticsChartContainer.bind('plothover', (event: any, pos: any, item: { dataIndex: number; datapoint: string[]; series: { data: string | any[]; }; pageX: any; pageY: any; }) => {
            if (!item) {
                return;
            }

            if (incomeStatisticsChartLastTooltipIndex !== item.dataIndex) {
                let label = '';
                const isSingleDaySelected = this.selectedDateRange.startDate.format('L') === this.selectedDateRange.endDate.format('L');
                if (this.selectedIncomeStatisticsDateInterval === AppIncomeStatisticsDateInterval.Daily ||
                    isSingleDaySelected) {
                    label = moment(item.datapoint[0]).format('dddd, DD MMMM YYYY');
                } else {
                    const isLastItem = item.dataIndex === item.series.data.length - 1;
                    label += moment(item.datapoint[0]).format('LL');
                    if (isLastItem) {
                        label += ' - ' + this.selectedDateRange.endDate.format('LL');
                    } else {
                        const nextItem = item.series.data[item.dataIndex + 1];
                        label += ' - ' + moment(nextItem[0]).format('LL');
                    }
                }

                incomeStatisticsChartLastTooltipIndex = item.dataIndex;
                const value = this.l('IncomeWithAmount', '<strong>' + item.datapoint[1] + '</strong>');
                showChartTooltip(item.pageX, item.pageY, label, value);
            }
        });

        incomeStatisticsChartContainer.bind('mouseleave', () => {
            incomeStatisticsChartLastTooltipIndex = null;
            removeChartTooltipIfExists();
        });
    }
}
