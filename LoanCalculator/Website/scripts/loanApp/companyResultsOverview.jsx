var LoanCompanyResultsOverview = React.createClass({
    render: function () {
        return (
            <div key={this.props.specificationCalculation.companyName} id="loanOverviewResults">
                <div className={"col-lg-4 col-md-5 col-xs-12 col-md-offset-1 information-box " + (this.props.specificationCalculation.overview === null ? "information-box-disabled" : "")}>
                    <div className="row">
                        <div className="col-md-12 information-box-header">
                            <h4>{this.props.specificationCalculation.companyName}</h4>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-sm-12 col-xs-12 company-result">
                            <table>
                                <tr className="company-result-value-row">
                                    <td className="company-result-name-row" title="Hvor meget af lånet der betales tilbage i løbet af det første år.">Afdrag 1.år</td>
                                    <td className="company-result-value-row" title="Hvor meget af lånet der betales tilbage i løbet af det første år.">{this.props.specificationCalculation.overview === null ? "0" : this.props.specificationCalculation.overview.firstYearRepayment.toLocaleString()} kr.</td>
                                </tr>
                                <tr>
                                    <td className="company-result-name-row" title="Rentebetalingen som betales i løbet af det første år.">Rente 1.år</td>
                                    <td className="company-result-value-row" title="Rentebetalingen som betales i løbet af det første år.">{this.props.specificationCalculation.overview === null ? "0" : this.props.specificationCalculation.overview.firstYearInterest.toLocaleString()} kr.</td>
                                </tr>
                                <tr>
                                    <td className="company-result-name-row" title="Gebyr realkreditinstitutet tager i løbet af det første år for at formidle lånet.">Bidrag 1.år</td>
                                    <td className="company-result-value-row" title="Gebyr realkreditinstitutet tager i løbet af det første år for at formidle lånet.">{this.props.specificationCalculation.overview === null ? "0" : this.props.specificationCalculation.overview.firstYearContribution.toLocaleString()} kr.</td>
                                </tr>
                                <tr>
                                    <td className="company-result-sum-row company-result-name-row" title="Den totale ydelse (afdrag, rente og bidrag lagt sammen) i løbet af det første år.">Total ydelse 1. år</td>
                                    <td className="company-result-sum-row company-result-value-row" title="Den totale ydelse (afdrag, rente og bidrag lagt sammen) det første år.">{this.props.specificationCalculation.overview === null ? "0" : this.props.specificationCalculation.overview.firstYearPayment.toLocaleString()} kr.</td>
                                </tr>
                            </table>
                        </div>

                        {/*
                        <div className="col-sm-3 col-xs-6 text-center">
                            <label title="Den totale ydelse (afdrag, rente og bidrag lagt sammen) i løbet af det første år.">Ydelse 1.år</label>
                            <p title="Den totale ydelse (afdrag, rente og bidrag lagt sammen) det første år.">{this.props.specificationCalculation.overview === null ? "0" : this.props.specificationCalculation.overview.firstYearPayment.toLocaleString()} kr.</p>
                        </div>
                        <div className="col-sm-3 col-xs-6 text-center">
                            <label title="Hvor meget af lånet der betales tilbage i løbet af det første år.">Afdrag 1.år</label>
                            <p title="Hvor meget af lånet der betales tilbage i løbet af det første år.">{this.props.specificationCalculation.overview === null ? "0" : this.props.specificationCalculation.overview.firstYearRepayment.toLocaleString()} kr.</p>
                        </div>
                        <div className="col-sm-3 col-xs-6 text-center">
                            <label title="Rentebetalingen som betales i løbet af det første år.">Rente 1.år</label>
                            <p title="Rentebetalingen som betales i løbet af det første år.">{this.props.specificationCalculation.overview === null ? "0" : this.props.specificationCalculation.overview.firstYearInterest.toLocaleString()} kr.</p>
                        </div>
                        <div className="col-sm-3 col-xs-6 text-center">
                            <label title="Gebyr realkreditinstitutet tager i løbet af det første år for at formidle lånet.">Bidrag 1.år</label>
                            <p title="Gebyr realkreditinstitutet tager i løbet af det første år for at formidle lånet.">{this.props.specificationCalculation.overview === null ? "0" : this.props.specificationCalculation.overview.firstYearContribution.toLocaleString()} kr.</p>
                        </div>
                        */}
                    </div>
                </div>
            </div>
        );
    }
});