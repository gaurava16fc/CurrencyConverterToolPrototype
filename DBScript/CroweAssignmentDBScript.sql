USE [CroweAssignment]
GO

IF OBJECT_ID(N'SourceAPIMaster') IS NOT NULL
	DROP TABLE [dbo].[SourceAPIMaster]
GO

CREATE TABLE [dbo].[SourceAPIMaster]
(
	ID							INT IDENTITY(1,1) PRIMARY KEY,
	SourceAPIName				VARCHAR(100) NOT NULL UNIQUE,
	SourceAPIClassName			VARCHAR(100) NOT NULL,
	SourceAPIURL				VARCHAR(500) NOT NULL,
	--SourceAPIQueryString		VARCHAR(300),
	IsFromCurrencyRequired		BIT DEFAULT(1) NOT NULL,
	IsToCurrencyRequired		BIT DEFAULT(1) NOT NULL,
	IsAmountRequired			BIT DEFAULT(0) NOT NULL,
	IsConversionDateRequired	BIT DEFAULT(0) NOT NULL,
	IsActiveAPI					BIT DEFAULT(1) NOT NULL
)
GO

INSERT INTO [dbo].[SourceAPIMaster] 
	(
		SourceAPIName, SourceAPIClassName, SourceAPIURL, IsFromCurrencyRequired, 
		IsToCurrencyRequired, IsAmountRequired, IsConversionDateRequired, IsActiveAPI				
	)
VALUES
	('CurrencyConverterAPI', 'CurrencyConverterAPI', N'https://free.currencyconverterapi.com/api/v5/convert?q={0}&compact=y', 
			1, 1, 0, 0, 1), -- FromCurrency_ToCurrency (QueryString Value's Format)
		('FixerAPIs', 'FixerAPI', N'http://api.fixer.io/latest?base={0}&symbols={1}', 
			1, 1, 0, 0, 0),	-- {0} UPPER(FromCurrency) / {1} UPPER(ToCurrency) (QueryString Value's Format)
	-- ADD THE REST THREE
	('OandaAPI', 'OandaAPIWrapper', N'http://www.global.oanda.finance.com/api/currencyconverter/', 
			1, 1, 0, 0, 1), 
	('XEAPI', 'XEAPIWrapper', N'http://www.xe.com/api/converter/', 
			1, 1, 0, 0, 1), 
	('YahooAPI', 'YahooAPIWrapper', N'http://www.finance.yahoo.com/d/currencyconverter/', 
			1, 1, 0, 0, 1)
GO

SELECT * FROM [dbo].[SourceAPIMaster]
GO
