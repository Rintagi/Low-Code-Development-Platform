IF EXISTS (SELECT i.name FROM sysindexes i INNER JOIN sysobjects o ON i.id = o.id WHERE i.name = 'IX_FxRate' AND o.name = 'FxRate')
    DROP INDEX FxRate.IX_FxRate 

CREATE  UNIQUE INDEX IX_FxRate ON FxRate(FrCurrency, ToCurrency, ValidFr)
GO