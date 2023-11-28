var LoanCompanyResultsDetailed = React.createClass({
    render: function () {
        var detailedResultRows = [];
        if (this.props.specificationCalculation !== undefined) {
            this.props.specificationCalculation.paymentPlan.forEach(function (paymentYear) {
                detailedResultRows.push(
                    <tr key={paymentYear.year}>
                        <td>{paymentYear.year}</td>
                        <td>{paymentYear.payment.toLocaleString()} kr.</td>
                        <td>{paymentYear.repayment.toLocaleString()} kr.</td>
                        <td className="hidden-xs">{paymentYear.interestRate.toLocaleString()} kr.</td>
                        <td className="hidden-xs">{paymentYear.contribution.toLocaleString()} kr.</td>
                        <td className="visible-xs">{(paymentYear.interestRate + paymentYear.contribution).toLocaleString()} kr.</td>
                        <td>{paymentYear.loanLeft.toLocaleString()} kr.</td>
                    </tr>);
    });
}

return (
    <div className="row" id="loanDetailedResults">
        <div className={"col-lg-8 col-md-10 col-md-offset-1 information-box " + (this.props.specificationCalculation === undefined ? "information-box-disabled" : "")}>
            <div className="row">
                <div className="col-md-12 information-box-header">
                    <h4>Detaljeret resultat</h4>
                </div>
            </div>
            <div className="row">
                <div className="col-sm-12">
                    <table className="table table-striped table-condensed table-responsive">
                        <thead>
                            <tr>
                                <th>År</th>
                                <th>Ydelse</th>
                                <th>Afdrag</th>
                                <th className="hidden-xs">Rente</th>
                                <th className="hidden-xs">Bidrag</th>
                                <th className="visible-xs">Rente+Bidrag</th>
                                <th>Restgæld</th>
                            </tr>
                        </thead>
                        <tbody>
                            {detailedResultRows}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
        );
}
});