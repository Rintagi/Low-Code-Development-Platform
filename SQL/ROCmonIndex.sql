IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_FxRate')
    DROP INDEX FxRate.IX_FxRate 

CREATE  UNIQUE INDEX IX_FxRate ON FxRate(FrCurrency, ToCurrency, ValidFr)
GO