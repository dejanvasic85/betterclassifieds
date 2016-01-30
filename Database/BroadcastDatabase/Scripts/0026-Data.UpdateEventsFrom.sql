update EmailTemplate 
set	[from] = 'events@kandobay.com.au'
where DocType like 'Event%'
and Brand = 'KandoBay'

