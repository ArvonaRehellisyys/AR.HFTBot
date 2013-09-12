# http://developer.yahoo.com/yql/console/?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22YHOO%22%2C%22AAPL%22%2C%22GOOG%22%2C%22MSFT%22)%0A%09%09&env=http%3A%2F%2Fdatatables.org%2Falltables.env#h=select+*+from+yahoo.finance.quotes+where+symbol+in+(%22NOK%22)%0A%09%09

function GetFromYahooApiToCsv($File, $Interval) {
    while($true) {

        $results = Invoke-WebRequest "http://query.yahooapis.com/v1/public/yql?q=select%20AskRealtime%2C%20BidRealtime%2C%20Symbol%20%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22PACQU%22%2C%22ERB%22%2C%22OCLR%22%2C%22PACQ%22%2C%20%22GALTW%22%2C%20%22PETX%22%2C%20%22MEIL%22%2C%20%22CREG%22%2C%20%22DSLV%22%2C%20%22CNIT%22%2C%20%22MARK%22%2C%20%22EZCH%22%2C%20%22TROVW%22%2C%20%22NUGT%22%2C%20%22USLV%22%2C%20%22PLX%22%2C%20%22ZA%22)%0A%09%09&env=http%3A%2F%2Fdatatables.org%2Falltables.env"
        $content = [xml]$results.Content;

        $data = $content.query.results.ChildNodes | Select-Object AskRealTime, BidRealTime, Symbol | Add-Member -NotePropertyValue (Get-Date) -NotePropertyName "Time" -PassThru

        $data | Export-Csv $File -Append

        Sleep $Interval
    }
}

GetFromYahooApiToCsv ".\Data\YahooApi.00.csv" (60*10)