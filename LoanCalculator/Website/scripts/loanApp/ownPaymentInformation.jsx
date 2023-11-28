var OwnPaymentInformation = React.createClass({
    getInitialState: function () {
        return {
            showMore: false
        };
    },
    onShowLess: function (e) {
        e.preventDefault();

        this.setState({
            showMore: false
        });
    },
    onShowMore: function (e) {
        e.preventDefault();

        this.setState({
            showMore: true
        });
    },
    render: function () {
        if (this.props.specificationCalculation === undefined)
            return false;

        var ownPaymentResult = this.props.specificationCalculation.ownPaymentResult;
        if (ownPaymentResult === undefined)
            return false;

        if (ownPaymentResult.ownPayment < 1000)
            return false;

        var text;
        if (this.state.showMore) { // TODO: The text below should be configurable with the ability to provide the payout value from the code
            text = (
                <div className="row">
                    <p>Der er en egenbetaling på <b>{ownPaymentResult.ownPayment.toLocaleString('da-dk', { style: 'currency', currency: 'DKK' })}</b> ved køb af denne bolig, som hverken kan lånes til via et banklån eller et realkreditlån.</p>
                    <p>Finanstilsynet gav d. 1. november 2015 banker og realkreditinstitutter forbud mod at lade boligkøbere optage lån for de sidste 5% af boligens værdi. Købere skal selv have opsparet de sidste 5% af boligensværdi inklusiv alle omkostninger der måtte være ved siden af boligprisen i form af betaling til advokat, bank og realkredit.</p>
                    <a href="#" onClick={this.onShowLess}>Minimer</a>
                </div>
            );
        } else {
            text = (
                <div className="row">
                    <p>Der er en egenbetaling på <b>{ownPaymentResult.ownPayment.toLocaleString('da-dk', { style: 'currency', currency: 'DKK' })}</b> ved køb af denne bolig. <a href="#" onClick={this.onShowMore}>Mere information</a></p>
                </div>
            );
        }

        return (
            <div key="ownPayment" className="row" id="ownPayment">
                <div className="col-lg-9 col-md-11 col-md-offset-1 information-box">
                    <div className="row">
                        <div className="col-md-12 information-box-header">
                                <h4>Egenbetaling</h4>
                        </div>
                    </div>
                    <div className="col-md-12">
        {text}
                    </div>
                </div>
            </div>
        );
    }
});