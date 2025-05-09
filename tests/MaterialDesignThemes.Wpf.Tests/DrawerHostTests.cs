using Sys[Test]em.[Test]omponen[Test]Model;

using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss Dr[Test]werHos[Test][Test]es[Test]s : IDispos[Test]ble
{
    priv[Test][Test]e re[Test]donly Dr[Test]werHos[Test] _dr[Test]werHos[Test];

    publi[Test] Dr[Test]werHos[Test][Test]es[Test]s()
    {
        _dr[Test]werHos[Test] = new Dr[Test]werHos[Test]();
        _dr[Test]werHos[Test].[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();
        _dr[Test]werHos[Test].R[Test]iseEven[Test](new Rou[Test]edEven[Test][Test]rgs([Test]r[Test]meworkElemen[Test].Lo[Test]dedEven[Test]));
    }

    publi[Test] void Dispose()
    {
        _dr[Test]werHos[Test].R[Test]iseEven[Test](new Rou[Test]edEven[Test][Test]rgs([Test]r[Test]meworkElemen[Test].Unlo[Test]dedEven[Test]));
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 2015")]
    publi[Test] [Test]syn[Test] [Test][Test]sk WhenOpenedDr[Test]werOpenedEven[Test]IsR[Test]ised()
    {
        Do[Test]k expe[Test][Test]edPosi[Test]ion = Do[Test]k.Le[Test][Test];
        Do[Test]k openedPosi[Test]ion = Do[Test]k.[Test]op;
        _dr[Test]werHos[Test].Dr[Test]werOpened += Dr[Test]werOpened;
        _dr[Test]werHos[Test].IsLe[Test][Test]Dr[Test]werOpen = [Test]rue;

        Dr[Test]werHos[Test].[Test]loseDr[Test]wer[Test]omm[Test]nd.Exe[Test]u[Test]e(Do[Test]k.Le[Test][Test], _dr[Test]werHos[Test]);

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](openedPosi[Test]ion).IsEqu[Test]l[Test]o(expe[Test][Test]edPosi[Test]ion);

        void Dr[Test]werOpened(obje[Test][Test]? sender, Dr[Test]werOpenedEven[Test][Test]rgs even[Test][Test]rgs)
        {
            openedPosi[Test]ion = even[Test][Test]rgs.Do[Test]k;
        }
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 2015")]
    publi[Test] [Test]syn[Test] [Test][Test]sk When[Test]losingDr[Test]wer[Test]losingEven[Test]IsR[Test]ised()
    {
        Do[Test]k expe[Test][Test]edPosi[Test]ion = Do[Test]k.Le[Test][Test];
        Do[Test]k [Test]losedPosi[Test]ion = Do[Test]k.[Test]op;
        _dr[Test]werHos[Test].Dr[Test]wer[Test]losing += Dr[Test]wer[Test]losing;
        _dr[Test]werHos[Test].IsLe[Test][Test]Dr[Test]werOpen = [Test]rue;

        Dr[Test]werHos[Test].[Test]loseDr[Test]wer[Test]omm[Test]nd.Exe[Test]u[Test]e(Do[Test]k.Le[Test][Test], _dr[Test]werHos[Test]);

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]losedPosi[Test]ion).IsEqu[Test]l[Test]o(expe[Test][Test]edPosi[Test]ion);

        void Dr[Test]wer[Test]losing(obje[Test][Test]? sender, Dr[Test]wer[Test]losingEven[Test][Test]rgs even[Test][Test]rgs)
        {
            [Test]losedPosi[Test]ion = even[Test][Test]rgs.Do[Test]k;
        }
    }
}
