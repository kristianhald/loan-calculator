var LoanResultsOverview = React.createClass({
    render: function () {
        var companiesOverview = [];
        if (this.props.specificationCalculation !== undefined && this.props.specificationCalculation.results !== undefined) {
            this.props.specificationCalculation.results.forEach(function (company) {
                companiesOverview.push(
                    <LoanCompanyResultsOverview key={company.companyName} specificationCalculation={ company } />);
            });
        };

        return (
            <div className="row">
                {companiesOverview}
            </div>);
    }
});